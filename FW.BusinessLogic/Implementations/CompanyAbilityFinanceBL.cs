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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyAbilityFinanceBL : BaseBL, ICompanyAbilityFinanceBL
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyAbilityFinanceRepository _iCompanyAbilityFinanceRepository;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly ICompanyProfileBL _iCompanyProfileBl;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        public CompanyAbilityFinanceBL(IUnitOfWork iUnitOfWork,
                                    ICompanyAbilityFinanceRepository iCompanyAbilityFinanceRepository,
                                    ICompanyRepository iCompanyRepository,
                                    ICompanyProfileBL iCompanyProfileBl,
                                    IAttachmentsToDOServices attachmentsToDOServices)
        {
            _iCompanyAbilityFinanceRepository = iCompanyAbilityFinanceRepository;
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _iCompanyProfileBl = iCompanyProfileBl;
            _attachmentsToDOServices = attachmentsToDOServices;
        }

        /// <summary>
        /// AddNewCompanyAbilityFinance
        /// </summary>
        /// <param name="companyAbilityFinanceVM"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JsonResult> AddNewCompanyAbilityFinance(CompanyAbilityFinanceVM companyAbilityFinanceVM, long? userId)
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

            companyAbilityFinanceVM.CompanyId = company.Id;
            var isExistInYear = await CheckUniqueInYear(companyAbilityFinanceVM.YearDeclare, company.Id);
            if (isExistInYear != null)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_THREE,
                        message = CommonConstants.NEED_UPDATE_COMPANY_INFO,
                        isExistInYear.Id,
                        userId
                    }
                };
            }

            var companyAbilityFinance = Mapper.Map<CompanyAbilityFinance>(companyAbilityFinanceVM);
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _iCompanyAbilityFinanceRepository.Add(companyAbilityFinance);

                // tra ve ket qua sau khi thao tac DB
                CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync().ConfigureAwait(false));
                companyAbilityFinance = await StoreCompanyAbilityFinanceFiles(companyAbilityFinance, companyAbilityFinanceVM, company.CompanyName).ConfigureAwait(false);
                _iCompanyAbilityFinanceRepository.Update(companyAbilityFinance);
                CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync().ConfigureAwait(false));
                transactionScope.Complete();
            }

            return new JsonResult
            {
                Data = new
                {
                    code = CommonConstants.STR_ZERO,
                    message = CommonConstants.ADD_SUCCESS,
                    userId
                }
            };
        }

        /// <summary>
        /// UpdateCompanyAbilityFinance
        /// </summary>
        /// <param name="companyAbilityFinanceVM"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateCompanyAbilityFinance(CompanyAbilityFinanceVM companyAbilityFinanceVM, long? userId)
        {
            var company = await _iCompanyRepository.ReadCompanyFromUserId(userId);
            companyAbilityFinanceVM.CompanyId = company.Id;
            var companyAbilityFinance = company.CompanyAbilityFinances.FirstOrDefault(x => x.YearDeclare == companyAbilityFinanceVM.YearDeclare);
            companyAbilityFinance = Mapper.Map(companyAbilityFinanceVM, companyAbilityFinance);
            companyAbilityFinance = await StoreCompanyAbilityFinanceFiles(companyAbilityFinance, companyAbilityFinanceVM, company.CompanyName).ConfigureAwait(false);

            _iCompanyAbilityFinanceRepository.Update(companyAbilityFinance);

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _iUnitOfWork.CommitAsync().ConfigureAwait(false);
            CheckDbExecutionResultAndThrowIfAny(result);

            return new JsonResult
            {
                Data = new
                {
                    code = CommonConstants.STR_ZERO,
                    message = CommonConstants.UPDATE_SUCCESS,
                    userId
                }
            };
        }

        /// <summary>
        /// ReadCompanyAbilityFinanceHasPagingByUserId
        /// </summary>
        /// <param name="iPaginationInfo"></param>
        /// <param name="userId"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompanyAbilityFinanceVM>> ReadCompanyAbilityFinanceHasPagingByUserId(
            IPaginationInfo iPaginationInfo, long? userId,
            List<SortOption> orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            var companyAbilityFinanceList = await _iCompanyAbilityFinanceRepository.ReadCompanyAbilityFinanceHasPagingByUserId(paginationInfo, userId);
            var companyAbilityFinanceListVM =
                Mapper.Map<IEnumerable<CompanyAbilityFinance>, IEnumerable<CompanyAbilityFinanceVM>>(
                    companyAbilityFinanceList);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return companyAbilityFinanceListVM;
        }

        /// <summary>
        /// DeleteCompanyAbilityFinance
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="typeOfAbility"></param>
        /// <returns></returns>
        public async Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityFinance(List<long?> listId, string typeOfAbility)
        {
            //Check id is existed in CompanyProfile
            var listIdStatus = _iCompanyProfileBl.CheckAbilityProfileExisted(listId, typeOfAbility);
            var resDeleteAbilityProfileVM = new List<ResponseDeleteAbilityProfileVM>();
            if (listIdStatus != null && listIdStatus.Any() && !listIdStatus.Any(l => l.Item2))
            {
                foreach (var idStatus in listIdStatus)
                {
                    var companyAbilityFinance = await _iCompanyAbilityFinanceRepository.GetAsync(x => x.Id == idStatus.Item1 && x.IsDeleted != true);
                    companyAbilityFinance.IsDeleted = true;
                    _iCompanyAbilityFinanceRepository.Update(companyAbilityFinance);
                    resDeleteAbilityProfileVM.Add(new ResponseDeleteAbilityProfileVM
                    {
                        Id = idStatus.Item1,
                        StatusDelete = StatusDelete.Success,
                        AbilityName = companyAbilityFinance.YearDeclare.ToString()
                    });
                }
            }
            else
            {
                resDeleteAbilityProfileVM.AddRange(from idStatus in listIdStatus.Where(x => x.Item2)
                                                   let companyAbilityFinance = _iCompanyAbilityFinanceRepository.Get(x => x.Id == idStatus.Item1 && x.IsDeleted != true)
                                                   select new ResponseDeleteAbilityProfileVM
                                                   {
                                                       Id = idStatus.Item1,
                                                       StatusDelete = StatusDelete.Existed,
                                                       AbilityName = companyAbilityFinance.YearDeclare.ToString()
                                                   });
            }

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _iUnitOfWork.CommitAsync().ConfigureAwait(false);
            CheckDbExecutionResultAndThrowIfAny(result);
            return resDeleteAbilityProfileVM;
        }


        /// <summary>
        /// CheckUniqueInYear
        /// </summary>
        /// <param name="year"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private Task<CompanyAbilityFinance> CheckUniqueInYear(int year, long? companyId)
            => _iCompanyAbilityFinanceRepository.GetAsync(ex => ex.CompanyId == companyId && ex.YearDeclare == year && ex.IsDeleted != true);

        public async Task<CompanyAbilityFinanceVM> ReadAllCompanyAbilityFinanceBy(long? id)
        {
            var companyAbilityFinance = await _iCompanyAbilityFinanceRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            var companyAbilityFinanceVM = Mapper.Map<CompanyAbilityFinanceVM>(companyAbilityFinance);
            return companyAbilityFinanceVM;
        }

        /// <summary>
        /// StorecompanyAbilityFinanceFiles
        /// </summary>
        /// <param name="companyAbilityFinance"></param>
        /// <param name="companyAbilityFinanceVM"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        private async Task<CompanyAbilityFinance> StoreCompanyAbilityFinanceFiles(CompanyAbilityFinance companyAbilityFinance, CompanyAbilityFinanceVM companyAbilityFinanceVM, string companyName)
        {
            if (companyAbilityFinanceVM.EvidenceCheckSettlementFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityFinanceVM.EvidenceCheckSettlementFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityFinance.EvidenceCheckSettlementFileName = companyAbilityFinanceVM.EvidenceCheckSettlementFile.FileName;
                companyAbilityFinance.EvidenceCheckSettlementFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (companyAbilityFinanceVM.EvidenceAuditReportFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityFinanceVM.EvidenceAuditReportFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityFinance.EvidenceAuditReportFileName = companyAbilityFinanceVM.EvidenceAuditReportFile.FileName;
                companyAbilityFinance.EvidenceAuditReportFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (companyAbilityFinanceVM.EvidenceCertificationTaxFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityFinanceVM.EvidenceCertificationTaxFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityFinance.EvidenceCertificationTaxFileName = companyAbilityFinanceVM.EvidenceCertificationTaxFile.FileName;
                companyAbilityFinance.EvidenceCertificationTaxFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }
            if (companyAbilityFinanceVM.EvidenceDeclareTaxFile?.ContentLength > 0)
            {
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(companyAbilityFinanceVM.EvidenceDeclareTaxFile);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                companyAbilityFinance.EvidenceDeclareTaxFileName = companyAbilityFinanceVM.EvidenceDeclareTaxFile.FileName;
                companyAbilityFinance.EvidenceDeclareTaxFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }

            return await Task.FromResult(companyAbilityFinance);
        }

        private static string GetStoragePath(string companyName, string id) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetCompanyAbilityFinanceFolderName, companyName, id);
    }
}
