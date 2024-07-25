using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FW.BusinessLogic.Interfaces;
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

namespace FW.BusinessLogic.Implementations
{
    public class CompanyAbilityExpBL : BaseBL, ICompanyAbilityExpBL
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyAbilityExpRepository _iCompanyAbilityExpRepository;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly ICompanyProfileBL _iCompanyProfileBl;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyAbilityExpBL(IUnitOfWork iUnitOfWork, ICompanyAbilityExpRepository iCompanyAbilityExpRepository, ICompanyRepository iCompanyRepository, ICompanyProfileBL iCompanyProfileBl, IUnitOfWork unitOfWork)
        {
            _iCompanyAbilityExpRepository = iCompanyAbilityExpRepository;
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _iCompanyProfileBl = iCompanyProfileBl;
            _unitOfWork = unitOfWork;
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
        private Task<CompanyAbilityExp> StoreCompanyAbilityExpFiles(CompanyAbilityExp companyAbilityExp, CompanyAbilityExpVM companyAbilityExpVM, string companyName)
        {
            if (companyAbilityExpVM.EvidenceBuildingPermitFile?.ContentLength > 0)
            {
                companyAbilityExp.EvidenceBuildingPermitFilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(companyAbilityExpVM.EvidenceBuildingPermitFile, GetStoragePath(companyName, companyAbilityExp.Id.ToString())),
                    FileUtils.GetDomainAppPathPath());
                companyAbilityExp.EvidenceBuildingPermitFileName = companyAbilityExpVM.EvidenceBuildingPermitFile.FileName;
            }
            if (companyAbilityExpVM.EvidenceContractFile?.ContentLength > 0)
            {
                companyAbilityExp.EvidenceContractFilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(companyAbilityExpVM.EvidenceContractFile, GetStoragePath(companyName, companyAbilityExp.Id.ToString())),
                    FileUtils.GetDomainAppPathPath());
                companyAbilityExp.EvidenceContractFileName = companyAbilityExpVM.EvidenceContractFile.FileName;
            }
            if (companyAbilityExpVM.EvidenceContractLiquidationFile?.ContentLength > 0)
            {
                companyAbilityExp.EvidenceContractLiquidationFilePath = StringUtils.GetRelativePath(FileUtils.SaveFileToServer(companyAbilityExpVM.EvidenceContractLiquidationFile, GetStoragePath(companyName, companyAbilityExp.Id.ToString())),
                    FileUtils.GetDomainAppPathPath());
                companyAbilityExp.EvidenceContractLiquidationFileName = companyAbilityExpVM.EvidenceContractLiquidationFile.FileName;
            }

            return Task.FromResult(companyAbilityExp);
        }

        private static string GetStoragePath(string companyName, string id) => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetCompanyAbilityExpFolderName, companyName, id);

    }
}
