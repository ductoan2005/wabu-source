using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.Services;
using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyAbilityHrBL : BaseBL, ICompanyAbilityHrBL
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyAbilityHrRepository _iCompanyAbilityHrRepository;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly ICompanyAbilityHrDetailRepository _iCompanyAbilityHrDetailRepository;
        private readonly ICompanyProfileBL _iCompanyProfileBl;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        public CompanyAbilityHrBL(IUnitOfWork iUnitOfWork,
                                ICompanyAbilityHrRepository iCompanyAbilityHrRepository,
                                ICompanyRepository iCompanyRepository,
                                ICompanyAbilityHrDetailRepository iCompanyAbilityHrDetailRepository,
                                ICompanyProfileBL iCompanyProfileBl,
                                IAttachmentsToDOServices attachmentsToDOServices)
        {
            _iCompanyAbilityHrRepository = iCompanyAbilityHrRepository;
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _iCompanyAbilityHrDetailRepository = iCompanyAbilityHrDetailRepository;
            _iCompanyProfileBl = iCompanyProfileBl;
            _attachmentsToDOServices = attachmentsToDOServices;
        }

        /// <summary>
        /// AddNewCompanyAbilityHr
        /// </summary>
        /// <param name="companyAbilityHrVM"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JsonResult> AddNewCompanyAbilityHr(CompanyAbilityHRVM companyAbilityHrVM, long? userId)
        {
            var company = await _iCompanyRepository.ReadCompanyFromUserId(userId);
            if (company == null)
                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_TWO,
                        message = CommonConstants.NEED_UPDATE_COMPANY_INFO,
                        userId
                    }
                };

            companyAbilityHrVM.CompanyId = company.Id;
            if (companyAbilityHrVM.CompanyAbilityHRDetails != null && companyAbilityHrVM.CompanyAbilityHRDetails.Any())
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                string format = "dd/MM/yyyy";
                foreach (var companyDetail in companyAbilityHrVM.CompanyAbilityHRDetails)
                {
                    companyDetail.FromYear = DateTime.ParseExact(companyDetail.FromYearString, format, provider);
                    companyDetail.ToYear = DateTime.ParseExact(companyDetail.ToYearString, format, provider);
                }
            }

            var companyAbilityHr = await _iCompanyAbilityHrRepository.GetByIdAsync(companyAbilityHrVM.Id);
            string returnMessage = string.Empty;
            if (companyAbilityHr == null)
            {
                companyAbilityHr = Mapper.Map<CompanyAbilityHR>(companyAbilityHrVM);
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    companyAbilityHr.CompanyAbilityHRDetails.ToList().ForEach(x =>
                    {
                        x.IsDeleted = false;
                        x.DateInserted = x.DateUpdated = DateTime.Now;
                    });
                    _iCompanyAbilityHrRepository.Add(companyAbilityHr);

                    // tra ve ket qua sau khi thao tac DB
                    await _iUnitOfWork.CommitAsync().ConfigureAwait(false);
                    companyAbilityHr = await StoreCompanyAbilityHrFiles(companyAbilityHr, companyAbilityHrVM, company.CompanyName).ConfigureAwait(false);
                    _iCompanyAbilityHrRepository.Update(companyAbilityHr);
                    CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync().ConfigureAwait(false));
                    transactionScope.Complete();
                }
                returnMessage = CommonConstants.ADD_SUCCESS;
            }
            else
            {
                companyAbilityHr = UpdateCompanyAbilityHRModel(companyAbilityHr, companyAbilityHrVM);
                companyAbilityHr = await StoreCompanyAbilityHrFiles(companyAbilityHr, companyAbilityHrVM, company.CompanyName).ConfigureAwait(false);
                var newEmployees = new List<CompanyAbilityHrDetailVM>();
                companyAbilityHrVM.CompanyAbilityHRDetails.ForEach(em =>
                {
                    if (em.Id == null)
                    {
                        em.CompanyAbilityHRId = companyAbilityHr.Id;
                        newEmployees.Add(em);
                    }
                    else
                    {
                        var updateEmployee = companyAbilityHr.CompanyAbilityHRDetails.FirstOrDefault(x => x.Id == em.Id);
                        if (updateEmployee != null)
                        {
                            updateEmployee.ProjectSimilar = em.ProjectSimilar;
                            updateEmployee.PositionSimilar = em.PositionSimilar;
                            updateEmployee.FromYear = em.FromYear;
                            updateEmployee.ToYear = em.ToYear;
                        }
                    }
                });

                var newEmployeesModel = Mapper.Map<List<CompanyAbilityHRDetail>>(newEmployees);
                _iCompanyAbilityHrDetailRepository.AddRange(newEmployeesModel);
                _iCompanyAbilityHrRepository.Update(companyAbilityHr);
                CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync().ConfigureAwait(false));
                returnMessage = CommonConstants.UPDATE_SUCCESS;
            }
            return new JsonResult
            {
                Data = new
                {
                    code = CommonConstants.STR_ZERO,
                    message = returnMessage,
                    userId
                }
            };
        }

        /// <summary>
        /// ReadCompanyAbilityHrHasPagingByUserId
        /// </summary>
        /// <param name="iPaginationInfo"></param>
        /// <param name="userId"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompanyAbilityHRVM>> ReadCompanyAbilityHrHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId,
            List<SortOption> orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            var companyAbilityHrList = await _iCompanyAbilityHrRepository.ReadCompanyAbilityHrHasPagingByUserId(paginationInfo, userId);
            var companyAbilityHrListVM =
                Mapper.Map<List<CompanyAbilityHR>, List<CompanyAbilityHRVM>>(companyAbilityHrList.ToList());
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return companyAbilityHrListVM;
        }

        /// <summary>
        /// DeleteCompanyAbilityHr
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="typeOfAbility"></param>
        /// <returns></returns>
        public async Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityHr(List<long?> listId, string typeOfAbility)
        {
            //Check id is existed in CompanyProfile
            var listIdStatus = _iCompanyProfileBl.CheckAbilityProfileExisted(listId, typeOfAbility);
            var resDeleteAbilityProfileVM = new List<ResponseDeleteAbilityProfileVM>();
            if (listIdStatus != null && listIdStatus.Any() && !listIdStatus.Any(l => l.Item2))
            {
                foreach (var idStatus in listIdStatus)
                {
                    var companyAbilityHr = await _iCompanyAbilityHrRepository.GetAsync(x => x.Id == idStatus.Item1 && x.IsDeleted != true);
                    companyAbilityHr.IsDeleted = true;
                    _iCompanyAbilityHrRepository.Update(companyAbilityHr);
                    var companyAbilityHrDetail =
                        _iCompanyAbilityHrDetailRepository.GetMany(ex => ex.CompanyAbilityHRId == idStatus.Item1);
                    if (companyAbilityHrDetail != null && companyAbilityHrDetail.Any())
                    {
                        foreach (var abilityHrDetail in companyAbilityHrDetail)
                        {
                            abilityHrDetail.IsDeleted = true;
                            _iCompanyAbilityHrDetailRepository.Update(abilityHrDetail);
                        }
                    }
                    resDeleteAbilityProfileVM.Add(new ResponseDeleteAbilityProfileVM
                    {
                        Id = idStatus.Item1,
                        StatusDelete = StatusDelete.Success,
                        AbilityName = companyAbilityHr.FullName
                    });
                }
            }
            else
            {
                resDeleteAbilityProfileVM.AddRange(from idStatus in listIdStatus.Where(x => x.Item2)
                                                   let companyAbilityHr = _iCompanyAbilityHrRepository.Get(x => x.Id == idStatus.Item1 && x.IsDeleted != true)
                                                   select new ResponseDeleteAbilityProfileVM
                                                   {
                                                       Id = idStatus.Item1,
                                                       StatusDelete = StatusDelete.Existed,
                                                       AbilityName = companyAbilityHr.FullName
                                                   });
            }

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _iUnitOfWork.CommitAsync().ConfigureAwait(false);
            CheckDbExecutionResultAndThrowIfAny(result);
            return resDeleteAbilityProfileVM;
        }
        public async Task<CompanyAbilityHRVM> ReadAllCompanyAbilityHrBy(long? Id)
        {
            var companyAbilityHr = await _iCompanyAbilityHrRepository.GetAsync(x => x.Id == Id && x.IsDeleted != true);
            var companyAbilityHrVM = Mapper.Map<CompanyAbilityHRVM>(companyAbilityHr);
            return companyAbilityHrVM;
        }

        /// <summary>
        /// StoreCompanyAbilityHrFiles
        /// </summary>
        /// <param name="companyAbilityHr"></param>
        /// <param name="companyAbilityHrVM"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        private async Task<CompanyAbilityHR> StoreCompanyAbilityHrFiles(CompanyAbilityHR companyAbilityHr, CompanyAbilityHRVM companyAbilityHrVM, string companyName)
        {
            if (companyAbilityHrVM.EvidenceAppointmentStaffFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityHrVM.EvidenceAppointmentStaffFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityHrVM.EvidenceAppointmentStaffFileName = companyAbilityHrVM.EvidenceAppointmentStaffFile.FileName;
                companyAbilityHrVM.EvidenceAppointmentStaffFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (companyAbilityHrVM.EvidenceLaborContractFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityHrVM.EvidenceLaborContractFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityHrVM.EvidenceLaborContractFileName = companyAbilityHrVM.EvidenceLaborContractFile.FileName;
                companyAbilityHrVM.EvidenceLaborContractFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (companyAbilityHrVM.EvidenceSimilarCertificatesFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityHrVM.EvidenceSimilarCertificatesFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityHrVM.EvidenceSimilarCertificatesFileName = companyAbilityHrVM.EvidenceSimilarCertificatesFile.FileName;
                companyAbilityHrVM.EvidenceSimilarCertificatesFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }

            return await Task.FromResult(companyAbilityHr);
        }

        private static string GetStoragePath(string companyName, string id) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetCompanyAbilityHrFolderName, companyName, id);

        private CompanyAbilityHR UpdateCompanyAbilityHRModel(CompanyAbilityHR companyAbilityHR, CompanyAbilityHRVM companyAbilityHRVM)
        {
            companyAbilityHR.Address = companyAbilityHRVM.Address;
            companyAbilityHR.Title = companyAbilityHRVM.Title;
            companyAbilityHR.Branch = companyAbilityHRVM.Branch;
            companyAbilityHR.Certificate = companyAbilityHRVM.Certificate;
            companyAbilityHR.FullName = companyAbilityHRVM.FullName;
            companyAbilityHR.PhoneNumber = companyAbilityHRVM.PhoneNumber;
            companyAbilityHR.School = companyAbilityHRVM.School;

            return companyAbilityHR;
        }

    }
}
