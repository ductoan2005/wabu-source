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
    public class CompanyAbilityExpBL : BaseBL, ICompanyAbilityExpBL
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyAbilityExpRepository _iCompanyAbilityExpRepository;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly ICompanyProfileBL _iCompanyProfileBl;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        public CompanyAbilityExpBL(IUnitOfWork iUnitOfWork,
                                ICompanyAbilityExpRepository iCompanyAbilityExpRepository,
                                ICompanyRepository iCompanyRepository,
                                ICompanyProfileBL iCompanyProfileBl,
                                IUnitOfWork unitOfWork,
                                IAttachmentsToDOServices attachmentsToDOServices)
        {
            _iCompanyAbilityExpRepository = iCompanyAbilityExpRepository;
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _iCompanyProfileBl = iCompanyProfileBl;
            _unitOfWork = unitOfWork;
            _attachmentsToDOServices = attachmentsToDOServices;
        }

        /// <summary>
        /// AddNewCompanyAbilityExp
        /// </summary>
        /// <param name="companyAbilityExpVM"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JsonResult> AddNewCompanyAbilityExp(CompanyAbilityExpVM companyAbilityExpVM, long? userId)
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

            companyAbilityExpVM.CompanyId = company.Id;
            var companyAbilityExp = await _iCompanyAbilityExpRepository.GetByIdAsync(companyAbilityExpVM.Id);
            string returnMessage = string.Empty;
            if (companyAbilityExp == null)
            {
                companyAbilityExp = Mapper.Map(companyAbilityExpVM, companyAbilityExp);

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _iCompanyAbilityExpRepository.Add(companyAbilityExp);
                    // tra ve ket qua sau khi thao tac DB
                    CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync());
                    companyAbilityExp = await StoreCompanyAbilityExpFiles(companyAbilityExp, companyAbilityExpVM, company.CompanyName);
                    _iCompanyAbilityExpRepository.Update(companyAbilityExp);
                    CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync());
                    transactionScope.Complete();
                }
                returnMessage = CommonConstants.ADD_SUCCESS;
            }
            else
            {
                companyAbilityExp = Mapper.Map(companyAbilityExpVM, companyAbilityExp);
                companyAbilityExp = await StoreCompanyAbilityExpFiles(companyAbilityExp, companyAbilityExpVM, company.CompanyName);
                _iCompanyAbilityExpRepository.Update(companyAbilityExp);
                CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync());
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

        public async Task<IEnumerable<CompanyAbilityExpVM>> ReadCompanyAbilityExpHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId,
            List<SortOption> orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            var companyAbilityExpList = await _iCompanyAbilityExpRepository.ReadCompanyAbilityExpHasPagingByUserId(paginationInfo, userId);
            var companyAbilityExpListVM =
                Mapper.Map<IEnumerable<CompanyAbilityExp>, IEnumerable<CompanyAbilityExpVM>>(companyAbilityExpList);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return companyAbilityExpListVM;
        }

        /// <summary>
        /// DeleteCompanyAbilityExp
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="typeOfAbility"></param>
        /// <returns></returns>
        public async Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityExp(List<long?> listId, string typeOfAbility)
        {
            //Check id is existed in CompanyProfile
            var listIdStatus = _iCompanyProfileBl.CheckAbilityProfileExisted(listId, typeOfAbility);
            var resDeleteAbilityProfileVM = new List<ResponseDeleteAbilityProfileVM>();
            if (listIdStatus != null && listIdStatus.Any() && !listIdStatus.Any(l => l.Item2))
            {
                foreach (var idStatus in listIdStatus)
                {
                    var companyAbilityExp = await _iCompanyAbilityExpRepository.GetAsync(x => x.Id == idStatus.Item1 && x.IsDeleted != true);
                    companyAbilityExp.IsDeleted = true;
                    _iCompanyAbilityExpRepository.Update(companyAbilityExp);
                    resDeleteAbilityProfileVM.Add(new ResponseDeleteAbilityProfileVM
                    {
                        Id = idStatus.Item1,
                        StatusDelete = StatusDelete.Success,
                        AbilityName = companyAbilityExp.ProjectName
                    });
                }
            }
            else
            {
                resDeleteAbilityProfileVM.AddRange(from idStatus in listIdStatus.Where(x => x.Item2)
                                                   let companyAbilityExp = _iCompanyAbilityExpRepository.Get(x => x.Id == idStatus.Item1 && x.IsDeleted != true)
                                                   select new ResponseDeleteAbilityProfileVM
                                                   {
                                                       Id = idStatus.Item1,
                                                       StatusDelete = StatusDelete.Existed,
                                                       AbilityName = companyAbilityExp.ProjectName
                                                   });
            }

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
            CheckDbExecutionResultAndThrowIfAny(result);
            return resDeleteAbilityProfileVM;
        }

        public IEnumerable<CompanyAbilityExpVM> ReadAllListCompanyAbilityExpBy(long? Id)
        {
            var companyAbilityExp = _iCompanyAbilityExpRepository.ReadAllCompanyAbilityExpBy(Id);
            var companyAbilityExpVM = Mapper.Map<IEnumerable<CompanyAbilityExp>, IEnumerable<CompanyAbilityExpVM>>(companyAbilityExp);
            return companyAbilityExpVM;
        }

        /// <summary>
        /// ReadAllCompanyAbilityExpBy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompanyAbilityExpVM> ReadAllCompanyAbilityExpBy(long? Id)
        {
            var companyAbilityExp = await _iCompanyAbilityExpRepository.GetAsync(x => x.Id == Id && x.IsDeleted != true);
            var companyAbilityExpVM = Mapper.Map<CompanyAbilityExpVM>(companyAbilityExp);
            return companyAbilityExpVM;
        }

        /// <summary>
        /// StoreCompanyAbilityExpFiles
        /// </summary>
        /// <param name="companyAbilityExp"></param>
        /// <param name="companyAbilityExpVM"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        private async Task<CompanyAbilityExp> StoreCompanyAbilityExpFiles(CompanyAbilityExp companyAbilityExp, CompanyAbilityExpVM companyAbilityExpVM, string companyName)
        {
            if (companyAbilityExpVM.EvidenceBuildingPermitFile?.ContentLength > 0)
            {
                var listIFormFile = new List<IFormFile>();
                listIFormFile.Add(FileUtils.ConvertToIFormFile(companyAbilityExpVM.EvidenceBuildingPermitFile));
                await _attachmentsToDOServices.DeleteAttachmentsToDO(listIFormFile.Select(x => x.FileName));
                await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);
                companyAbilityExp.EvidenceBuildingPermitFileName = companyAbilityExpVM.EvidenceBuildingPermitFile.FileName;
                companyAbilityExp.EvidenceBuildingPermitFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + companyAbilityExp.EvidenceBuildingPermitFileName;
            }
            if (companyAbilityExpVM.EvidenceContractFile?.ContentLength > 0)
            {
                var listIFormFile = new List<IFormFile>();
                listIFormFile.Add(FileUtils.ConvertToIFormFile(companyAbilityExpVM.EvidenceContractFile));
                await _attachmentsToDOServices.DeleteAttachmentsToDO(listIFormFile.Select(x => x.FileName));
                await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);
                companyAbilityExp.EvidenceContractFileName = companyAbilityExpVM.EvidenceContractFile.FileName;
                companyAbilityExp.EvidenceContractFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + companyAbilityExp.EvidenceContractFileName;
            }
            if (companyAbilityExpVM.EvidenceContractLiquidationFile?.ContentLength > 0)
            {
                var listIFormFile = new List<IFormFile>();
                listIFormFile.Add(FileUtils.ConvertToIFormFile(companyAbilityExpVM.EvidenceContractLiquidationFile));
                await _attachmentsToDOServices.DeleteAttachmentsToDO(listIFormFile.Select(x => x.FileName));
                await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);
                companyAbilityExp.EvidenceContractLiquidationFileName = companyAbilityExpVM.EvidenceContractLiquidationFile.FileName;
                companyAbilityExp.EvidenceContractLiquidationFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + companyAbilityExp.EvidenceContractLiquidationFileName;
            }

            return await Task.FromResult(companyAbilityExp);
        }

        private static string GetStoragePath(string companyName, string id) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetCompanyAbilityExpFolderName, companyName, id);

    }
}
