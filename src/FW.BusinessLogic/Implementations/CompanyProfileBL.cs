using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;

namespace FW.BusinessLogic.Implementations
{
    public class CompanyProfileBL : BaseBL, ICompanyProfileBL
    {
        private readonly ICompanyProfileRepository _iCompanyProfileRepository;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly IBiddingDetailRepository _iBiddingDetailRepository;
        private readonly ICompanyAbilityExpRepository _companyAbilityExpRepository;
        private readonly ICompanyAbilityHrRepository _companyAbilityHrRepository;
        private readonly ICompanyAbilityFinanceRepository _companyAbilityFinanceRepository;
        private readonly ICompanyAbilityEquipmentRepository _companyAbilityEquipmentRepository;

        public CompanyProfileBL(ICompanyProfileRepository iCompanyProfileRepository, IUnitOfWork iUnitOfWork, ICompanyRepository iCompanyRepository,
            IBiddingDetailRepository iBiddingDetailRepository, ICompanyAbilityExpRepository companyAbilityExpRepository, ICompanyAbilityHrRepository companyAbilityHrRepository,
            ICompanyAbilityFinanceRepository companyAbilityFinanceRepository, ICompanyAbilityEquipmentRepository companyAbilityEquipmentRepository)
        {
            _iCompanyProfileRepository = iCompanyProfileRepository;
            _iUnitOfWork = iUnitOfWork;
            _iCompanyRepository = iCompanyRepository;
            _iBiddingDetailRepository = iBiddingDetailRepository;
            _companyAbilityExpRepository = companyAbilityExpRepository;
            _companyAbilityHrRepository = companyAbilityHrRepository;
            _companyAbilityFinanceRepository = companyAbilityFinanceRepository;
            _companyAbilityEquipmentRepository = companyAbilityEquipmentRepository;
        }

        public virtual List<CompanyProfileVM> ReadAllCompanyProfiles(UserProfile userProfile, IPaginationInfo iPaginationInfo, long? biddingNewsId = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };
            var companyProfilesList = _iCompanyProfileRepository.ReadAllCompanyProfilesHasPaging(userProfile, paginationInfo);
            var companyProfileVM = Mapper.Map<IEnumerable<CompanyProfile>, List<CompanyProfileVM>>(companyProfilesList);
            iPaginationInfo.TotalItems = paginationInfo.TotalItems;

            if (biddingNewsId == null)
                return companyProfileVM;

            foreach (var companyProfile in companyProfileVM)
            {
                companyProfile.IsBidding =
                    _iBiddingDetailRepository.CheckCompanyProfileHasBidding(biddingNewsId, companyProfile.Id);
            }

            return companyProfileVM;
        }

        public async Task<JsonResult> AddNewCompanyProfile(CompanyProfileVM companyProfileVM, long? userId)
        {
            var companyProfile = await _iCompanyProfileRepository.GetByIdAsync(companyProfileVM.Id);
            string returnMessage = string.Empty;
            if(companyProfile == null)
            {
                var company = await _iCompanyRepository.ReadCompanyFromUserId(userId);
                companyProfileVM.CompanyId = company.Id;
                companyProfileVM.Company = company;
                companyProfile = Mapper.Map<CompanyProfile>(companyProfileVM);
                companyProfile.IsDeleted = false;
                _iCompanyProfileRepository.Add(companyProfile);
                returnMessage = CommonConstants.ADD_SUCCESS;
            }
            else
            {
                companyProfile.NameProfile = companyProfileVM.NameProfile;
                companyProfile.AbilityEquipmentsId = companyProfileVM.AbilityEquipmentsId;
                companyProfile.AbilityExpsId = companyProfileVM.AbilityExpsId;
                companyProfile.AbilityFinancesId = companyProfileVM.AbilityFinancesId;
                companyProfile.AbilityHRsId = companyProfileVM.AbilityHRsId;
                _iCompanyProfileRepository.Update(companyProfile);
                returnMessage = CommonConstants.UPDATE_SUCCESS;
            }

            DbExecutionResult resultCompanyProfile = await _iUnitOfWork.CommitAsync();
            CheckDbExecutionResultAndThrowIfAny(resultCompanyProfile);

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

        public IEnumerable<Tuple<long?, bool>> CheckAbilityProfileExisted(List<long?> listId, string typeOfAbility)
        {
            IEnumerable<string> listIdExistedFromDb = null;
            if (typeOfAbility.Equals(typeof(CompanyAbilityExp).Name))
            {
                listIdExistedFromDb = _iCompanyProfileRepository.GetMany(ex => ex.IsDeleted != true && !string.IsNullOrEmpty(ex.AbilityExpsId)).Select(ex => ex.AbilityExpsId).Distinct();
            }
            else if (typeOfAbility.Equals(typeof(CompanyAbilityFinance).Name))
            {
                listIdExistedFromDb = _iCompanyProfileRepository.GetMany(ex => ex.IsDeleted != true && !string.IsNullOrEmpty(ex.AbilityFinancesId)).Select(ex => ex.AbilityFinancesId).Distinct();
            }
            else if (typeOfAbility.Equals(typeof(CompanyAbilityEquipment).Name))
            {
                listIdExistedFromDb = _iCompanyProfileRepository.GetMany(ex => ex.IsDeleted != true && !string.IsNullOrEmpty(ex.AbilityEquipmentsId)).Select(ex => ex.AbilityEquipmentsId).Distinct();
            }
            else if (typeOfAbility.Equals(typeof(CompanyAbilityHR).Name))
            {
                listIdExistedFromDb = _iCompanyProfileRepository.GetMany(ex => ex.IsDeleted != true && !string.IsNullOrEmpty(ex.AbilityHRsId)).Select(ex => ex.AbilityHRsId).Distinct();
            }
            var statusListId = CheckIdIsExistedLogic(listId, listIdExistedFromDb);

            return statusListId;
        }

        private IEnumerable<Tuple<long?, bool>> CheckIdIsExistedLogic(IEnumerable<long?> listId,
            IEnumerable<string> listIdExistedFromDb)
        {
            var listIdExisted = new List<long?>();
            foreach (var idExisted in listIdExistedFromDb)
            {
                var listIdStr = idExisted.Split(',');
                foreach (var idStr in listIdStr)
                {
                    var idLong = Convert.ToInt64(idStr);
                    if (!listIdExisted.Contains(idLong))
                    {
                        listIdExisted.Add(idLong);
                    }
                }
            }
            return listId.Select(id => listIdExisted.Any(idExisted => id == idExisted)
                    ? new Tuple<long?, bool>(id, true)
                    : new Tuple<long?, bool>(id, false));
        }

        /// <summary>
        /// GetCompanyProfileById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompanyProfileVM> GetCompanyProfileById(long? id)
        {
            var companyprofile = await _iCompanyProfileRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            var companyProfileVM = Mapper.Map<CompanyProfileVM>(companyprofile);
            return companyProfileVM;
        }

        public async Task<CompanyAbilityExpVM> GetCompanyAbilityExpDetailById(long? id)
        {
            var companyAbilityExp = await _companyAbilityExpRepository.GetByIdAsync(id);
            var companyAbilityExpVM = Mapper.Map<CompanyAbilityExpVM>(companyAbilityExp);
            return companyAbilityExpVM;
        }

        public async Task<CompanyAbilityEquipmentVM> GetCompanyAbilityEquipmentDetailById(long? id)
        {
            var companyAbilityEquipment = await _companyAbilityEquipmentRepository.GetByIdAsync(id);
            var companyAbilityEquipmentVM = Mapper.Map<CompanyAbilityEquipmentVM>(companyAbilityEquipment);
            return companyAbilityEquipmentVM;
        }

        public async Task<CompanyAbilityHRVM> GetCompanyAbilityExpHrDetailById(long? id)
        {
            var companyAbilityHR = await _companyAbilityHrRepository.GetByIdAsync(id);
            var companyAbilityHRVM = Mapper.Map<CompanyAbilityHRVM>(companyAbilityHR);
            return companyAbilityHRVM;
        }

        public async Task<CompanyAbilityFinanceVM> GetCompanyAbilityFinanceDetailById(long? id)
        {
            var companyAbilityFinance = await _companyAbilityFinanceRepository.GetByIdAsync(id);
            var companyAbilityFinanceVM = Mapper.Map<CompanyAbilityFinanceVM>(companyAbilityFinance);
            return companyAbilityFinanceVM;
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
