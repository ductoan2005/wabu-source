using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class BiddingNewsBL : BaseBL, IBiddingNewsBL
    {
        #region Constants

        internal const string ORDER_BY_DEFAULT = "";

        #endregion

        #region Fields

        private readonly IBiddingNewsBookmarkRepository _biddingNewsBookmarkRepository;

        private readonly IBiddingNewsRepository _biddingNewsRepository;

        private readonly IBiddingPackageRepository _iBiddingPackageRepository;

        private readonly IBiddingNewsDetailRepository _biddingNewsDetailRepository;


        private readonly ICompanyProfileRepository _iCompanyProfileRepository;

        private readonly ICompanyRepository _companyRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IBiddingDetailRepository _biddingDetailRepository;

        #endregion

        #region Ctor

        public BiddingNewsBL(IBiddingNewsBookmarkRepository biddingNewsBookmarkRepository, IBiddingNewsRepository biddingNewsRepository,
            IBiddingPackageRepository iBiddingPackageRepository, IBiddingNewsDetailRepository biddingNewsDetailRepository,
            ICompanyProfileRepository iCompanyProfileRepository, ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IBiddingDetailRepository biddingDetailRepository)
        {
            _biddingNewsBookmarkRepository = biddingNewsBookmarkRepository;
            _biddingNewsRepository = biddingNewsRepository;
            _iBiddingPackageRepository = iBiddingPackageRepository;
            _biddingNewsDetailRepository = biddingNewsDetailRepository;
            _iCompanyProfileRepository = iCompanyProfileRepository;
            _companyRepository = companyRepository;
            _biddingDetailRepository = biddingDetailRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// ReadBiddingNewsToPagingByCondition
        /// </summary>
        /// <param name="iPaginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public IEnumerable<BiddingNewsVM> ReadBiddingNewsToPagingByCondition(IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            var biddingNews = _biddingNewsRepository.ReadBiddingNewsToPagingByCondition(paginationInfo, userProfile, condition, orderByStr);

            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return biddingNews;
        }

        /// <summary>
        /// GetSelectListBiddingPackage
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelectListBiddingPackage()
        {
            var result = _iBiddingPackageRepository.GetAll().Where(a => a.IsDeleted != true && a.BiddingPackageType == 4)
                .Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.BiddingPackageName
                    }).Distinct();

            return result;
        }

        /// <summary>
        /// CheckContructionByProfileId
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public bool CheckContructionByProfileId(long? profileId)
        {
            bool contruction = _biddingNewsDetailRepository.CheckContructionByProfileId(profileId);
            return contruction;
        }
        /// <summary>
        /// checkBiddingNewsSelected
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        public bool checkBiddingNewsSelected(long? biddingNewsId)
        {
            bool biddingnewsselected = _biddingDetailRepository.CheckInvestorIsSelectContractor(biddingNewsId);
            return biddingnewsselected;
        }
        public bool checkBiddingNewsCompleted(long? biddingNewsId)
        {
            bool biddingnewscompleted = _biddingNewsRepository.CheckInvestorIsCompletedContractor(biddingNewsId);
            return biddingnewscompleted;
        }

        /// <summary>
        /// CheckBiddingNewsProfileShowCondition
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        public async Task<BidShowContractDetailVM> CheckBiddingNewsProfileShowCondition(long? biddingNewsId, long? companyProfileId)
        {
            var bidShowContractDetailVM = new BidShowContractDetailVM();
            var biddingNew = await _biddingNewsRepository.GetAsync(x => x.Id == biddingNewsId && x.IsDeleted != true);
            if (biddingNew != null)
            {
                bidShowContractDetailVM.BiddingNews = biddingNew;
                bidShowContractDetailVM.CheckBiddingNewsCompleted = biddingNew.StatusBiddingNews == 3;
                bidShowContractDetailVM.CheckBiddingNewsSelected = biddingNew.BiddingDetails.Any(x => x.InvestorSelected && x.IsDeleted != true);
                bidShowContractDetailVM.CheckConstructionByCompanyProfile = biddingNew.BiddingDetails.Any(x => x.CompanyProfileId == companyProfileId && x.IsDeleted != true);
                BiddingDetail biddingDetail = biddingNew.BiddingDetails.FirstOrDefault(x => x.IsDeleted != true && x.BiddingNewsId == biddingNewsId && x.CompanyProfileId == companyProfileId);

                if (biddingDetail != null)
                    bidShowContractDetailVM.ListFileWithProfile = Mapper.Map<BiddingDetailVM>(biddingDetail);
            }

            return bidShowContractDetailVM;
        }

        /// <summary>
        /// Read Bidding News By Id
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<BiddingNewsBidContractionDetailVM> ReadBiddingNewsById(long? biddingNewsId, UserProfile user)
        {
            var biddingNews = await _biddingNewsRepository.GetAsync(x => x.Id == biddingNewsId && x.IsDeleted != true).ConfigureAwait(false);

            var result = Mapper.Map<BiddingNews, BiddingNewsBidContractionDetailVM>(biddingNews);
            result.CompanyProfile = user == null ? null : (await _companyRepository.GetWithConditionAsync(x => x.UserId == user.UserID))?.CompanyProfiles;
            if (result.Construction.UserId == user?.UserID)
            {
                result.BiddingDetails = result.BiddingDetails.OrderBy(o => o.Price).ToList();
            }

            return result;
        }

        /// <summary>
        /// SearchBiddingNewsByCondition
        /// </summary>
        /// <param name="iPaginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BiddingNewsVM>> SearchBiddingNewsByCondition(IPaginationInfo iPaginationInfo,
            UserProfile userProfile, string condition, string orderByStr = null)
        {

            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            var biddingNewsList = await _biddingNewsRepository.SearchBiddingNewsByCondition(paginationInfo, userProfile, condition, orderByStr).ConfigureAwait(false);

            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return biddingNewsList;

        }

        /// <summary>
        /// GetMaxTurnover2YearAbilityFinance
        /// </summary>
        /// <returns></returns>
        public Task<long> GetMaxTurnover2YearAbilityFinance() => _biddingNewsRepository.GetMaxTurnover2YearAbilityFinance();


        public async Task<IList<BiddingNewsVM>> FilterBiddingNewsByCondition(bool isFilterValidNews, IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            var biddingNewsList = (await _biddingNewsRepository.FilterBiddingNewsByCondition(isFilterValidNews, paginationInfo, userProfile, condition, orderByStr)).ToList();

            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return biddingNewsList;
        }

        public async Task<bool> UpdateActiveStatus(long id, bool isActive)
        {

            var biddingNew = await _biddingNewsRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            // check data is deleted
            if (biddingNew.IsDeleted == true)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_RECORD_IS_DELETED);
            }

            // Update data
            biddingNew.IsActived = isActive;
            biddingNew.NewsApprovalDate = DateTime.Now;

            // excute update
            _biddingNewsRepository.Update(biddingNew);

            // save data to database and verify data
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

            return isActive;
        }

        public async Task DeleteNews(long id)
        {

            var biddingNew = await _biddingNewsRepository.GetAsync(x => x.Id == id && x.IsDeleted != true);
            // check data is deleted
            if (biddingNew.IsDeleted == true)
            {
                throw new CommonExceptions(CommonResource.MSG_ERROR_RECORD_IS_DELETED);
            }

            // Update data
            biddingNew.IsDeleted = true;

            // excute update
            _biddingNewsRepository.Update(biddingNew);

            // save data to database and verify data
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));
        }

        public async Task<JsonResult> SaveBiddingNewsBookmark(long? biddingNewsId, UserProfile userProfile)
        {
            var biddingNewsBookmark = new BiddingNewsBookmark
            {
                BiddingNewsId = biddingNewsId,
                UserId = userProfile.UserID,
                BookmarkDate = DateTime.Now
            };
            // excute update
            _biddingNewsBookmarkRepository.Add(biddingNewsBookmark);

            // save data to database and verify data
            CheckDbExecutionResultAndThrowIfAny(await _unitOfWork.CommitAsync().ConfigureAwait(false));

            return new JsonResult
            {
                Data = new
                {
                    succeed = "0"
                }
            };
        }

        #endregion

    }
}
