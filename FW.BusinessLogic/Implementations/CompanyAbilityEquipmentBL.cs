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
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyAbilityEquipmentBL : BaseBL, ICompanyAbilityEquipmentBL
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyAbilityEquipmentRepository _iCompanyAbilityEquipmentRepository;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly ICompanyProfileBL _iCompanyProfileBl;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        public CompanyAbilityEquipmentBL(IUnitOfWork iUnitOfWork, ICompanyAbilityEquipmentRepository iCompanyAbilityEquipmentRepository,
            ICompanyRepository iCompanyRepository, ICompanyProfileBL iCompanyProfileBl, IAttachmentsToDOServices attachmentsToDOServices)
        {
            _iCompanyAbilityEquipmentRepository = iCompanyAbilityEquipmentRepository;
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _iCompanyProfileBl = iCompanyProfileBl;
            _attachmentsToDOServices = attachmentsToDOServices;
        }

        public async Task<JsonResult> AddNewCompanyAbilityEquipment(CompanyAbilityEquipmentVM companyAbilityEquipmentVM, long? userId)
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

            companyAbilityEquipmentVM.CompanyId = company.Id;
            var companyAbilityEquipment = await _iCompanyAbilityEquipmentRepository.GetByIdAsync(companyAbilityEquipmentVM.Id);
            string returnMessage = string.Empty;


            if (companyAbilityEquipment == null)
            {
                companyAbilityEquipment = Mapper.Map(companyAbilityEquipmentVM, companyAbilityEquipment);
                companyAbilityEquipment.Source = GetCompanyAbilityEquipmentSource(companyAbilityEquipmentVM.Source);
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _iCompanyAbilityEquipmentRepository.Add(companyAbilityEquipment);
                    // tra ve ket qua sau khi thao tac DB
                    CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync().ConfigureAwait(false));
                    companyAbilityEquipment = await StoreCompanyAbilityEquipmentFiles(companyAbilityEquipment, companyAbilityEquipmentVM, company.CompanyName).ConfigureAwait(false);
                    _iCompanyAbilityEquipmentRepository.Update(companyAbilityEquipment);
                    CheckDbExecutionResultAndThrowIfAny(await _iUnitOfWork.CommitAsync().ConfigureAwait(false));
                    transactionScope.Complete();
                }
                returnMessage = CommonConstants.ADD_SUCCESS;

            }
            else
            {
                companyAbilityEquipment = Mapper.Map(companyAbilityEquipmentVM, companyAbilityEquipment);
                companyAbilityEquipment.Source = GetCompanyAbilityEquipmentSource(companyAbilityEquipmentVM.Source);
                companyAbilityEquipment = await StoreCompanyAbilityEquipmentFiles(companyAbilityEquipment, companyAbilityEquipmentVM, company.CompanyName).ConfigureAwait(false);
                _iCompanyAbilityEquipmentRepository.Update(companyAbilityEquipment);
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

        public async Task<IEnumerable<CompanyAbilityEquipmentVM>> ReadCompanyAbilityEquipmentHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId,
            List<SortOption> orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            var companyAbilityEquipmentList = await _iCompanyAbilityEquipmentRepository.ReadCompanyAbilityFinanceHasPagingByUserId(paginationInfo, userId);
            var companyAbilityEquipmentListVM =
                Mapper.Map<IEnumerable<CompanyAbilityEquipment>, IEnumerable<CompanyAbilityEquipmentVM>>(companyAbilityEquipmentList);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return companyAbilityEquipmentListVM;
        }

        public async Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityEquipment(List<long?> listId, string typeOfAbility)
        {
            //Check id is existed in CompanyProfile
            var listIdStatus = _iCompanyProfileBl.CheckAbilityProfileExisted(listId, typeOfAbility);
            var resDeleteAbilityProfileVM = new List<ResponseDeleteAbilityProfileVM>();
            if (listIdStatus != null && listIdStatus.Any() && !listIdStatus.Any(l => l.Item2))
            {
                foreach (var idStatus in listIdStatus)
                {
                    var companyAbilityEquipment = await _iCompanyAbilityEquipmentRepository.GetAsync(x => x.Id == idStatus.Item1 && x.IsDeleted != true);
                    companyAbilityEquipment.IsDeleted = true;
                    _iCompanyAbilityEquipmentRepository.Update(companyAbilityEquipment);
                    resDeleteAbilityProfileVM.Add(new ResponseDeleteAbilityProfileVM
                    {
                        Id = idStatus.Item1,
                        StatusDelete = StatusDelete.Success,
                        AbilityName = companyAbilityEquipment.EquipmentType
                    });
                }
            }
            else
            {
                resDeleteAbilityProfileVM.AddRange(from idStatus in listIdStatus.Where(x => x.Item2)
                                                   let companyAbilityEquipment = _iCompanyAbilityEquipmentRepository.Get(x => x.Id == idStatus.Item1 && x.IsDeleted != true)
                                                   select new ResponseDeleteAbilityProfileVM
                                                   {
                                                       Id = idStatus.Item1,
                                                       StatusDelete = StatusDelete.Existed,
                                                       AbilityName = companyAbilityEquipment.EquipmentType
                                                   });
            }

            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _iUnitOfWork.CommitAsync().ConfigureAwait(false);
            CheckDbExecutionResultAndThrowIfAny(result);
            return resDeleteAbilityProfileVM;
        }
        public async Task<CompanyAbilityEquipmentVM> ReadAllCompanyAbilityEquipmentBy(long? id)
        {
            var companyAbilityEquipment = await _iCompanyAbilityEquipmentRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            var companyAbilityEquipmentVM = Mapper.Map<CompanyAbilityEquipmentVM>(companyAbilityEquipment);
            return companyAbilityEquipmentVM;
        }

        /// <summary>
        /// StoreCompanyAbilityEquipmentFiles
        /// </summary>
        /// <param name="companyAbilityEquipment"></param>
        /// <param name="companyAbilityEquipmentVM"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        private async Task<CompanyAbilityEquipment> StoreCompanyAbilityEquipmentFiles(CompanyAbilityEquipment companyAbilityEquipment, CompanyAbilityEquipmentVM companyAbilityEquipmentVM, string companyName)
        {
            if (companyAbilityEquipmentVM.EvidenceInspectionRecordsFile?.ContentLength > 0)
            {
                var listIFormFile = new List<IFormFile>();
                listIFormFile.Add(FileUtils.ConvertToIFormFile(companyAbilityEquipmentVM.EvidenceInspectionRecordsFile));
                await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);
                companyAbilityEquipment.EvidenceInspectionRecordsFileName = companyAbilityEquipmentVM.EvidenceInspectionRecordsFile.FileName;
                companyAbilityEquipment.EvidenceInspectionRecordsFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + companyAbilityEquipment.EvidenceInspectionRecordsFileName;
            }
            if (companyAbilityEquipmentVM.EvidenceSaleContractFile?.ContentLength > 0)
            {
                var listIFormFile = new List<IFormFile>();
                listIFormFile.Add(FileUtils.ConvertToIFormFile(companyAbilityEquipmentVM.EvidenceSaleContractFile));
                await _attachmentsToDOServices.UploadAttachmentsToDO(listIFormFile);
                companyAbilityEquipment.EvidenceSaleContractFileName = companyAbilityEquipmentVM.EvidenceSaleContractFile.FileName;
                companyAbilityEquipment.EvidenceSaleContractFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + companyAbilityEquipment.EvidenceSaleContractFileName;
            }

            return await Task.FromResult(companyAbilityEquipment);
        }

        private static string GetStoragePath(string companyName, string id)
            => Path.Combine(FileUtils.GetServerStoragePath(), CommonSettings.GetCompanyAbilityEquipmentFolderName, companyName, id);

        public static string GetCompanyAbilityEquipmentSource(string source)
        {
            string result = string.Empty;
            switch (source)
            {
                case "1":
                    result = ESourceEquiment.IsBiddingOwner.GetType()
                                                                    .GetMember(ESourceEquiment.IsBiddingOwner.ToString())
                                                                    .First()
                                                                    .GetCustomAttribute<DisplayAttribute>()
                                                                    .GetName();
                    break;
                case "2":
                    result = ESourceEquiment.IsBiddingHire.GetType()
                                                                    .GetMember(ESourceEquiment.IsBiddingHire.ToString())
                                                                    .First()
                                                                    .GetCustomAttribute<DisplayAttribute>()
                                                                    .GetName();

                    break;
                case "3":
                    result = ESourceEquiment.IsBiddingSpecialManufacture.GetType()
                                                                    .GetMember(ESourceEquiment.IsBiddingSpecialManufacture.ToString())
                                                                    .First()
                                                                    .GetCustomAttribute<DisplayAttribute>()
                                                                    .GetName();
                    break;
                default:
                    break;
            }

            return result;
        }

        public List<SelectListItem> CreateAbilityEquipmentSourceListItem()
        {
            var listItem = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "",
                    Text = "Chọn hình thức hợp đồng"
                },
                new SelectListItem
                {
                    Value = "1",
                    Text = ESourceEquiment.IsBiddingOwner.GetType().GetMember(ESourceEquiment.IsBiddingOwner.ToString())
                                                                    .First()
                                                                    .GetCustomAttribute<DisplayAttribute>()
                                                                    .GetName()
                },
                new SelectListItem
                {
                    Value = "2",
                    Text = ESourceEquiment.IsBiddingHire.GetType().GetMember(ESourceEquiment.IsBiddingHire.ToString())
                                                                    .First()
                                                                    .GetCustomAttribute<DisplayAttribute>()
                                                                    .GetName()
                },
                new SelectListItem
                {
                    Value = "3",
                    Text = ESourceEquiment.IsBiddingSpecialManufacture.GetType().GetMember(ESourceEquiment.IsBiddingSpecialManufacture.ToString())
                                                                    .First()
                                                                    .GetCustomAttribute<DisplayAttribute>()
                                                                    .GetName()
                }
            };

            return listItem;
        }
    }
}
