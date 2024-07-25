using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Pagination.Interfaces;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;

namespace FW.BusinessLogic.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IBiddingNewsBL
    {
        /// <summary>
        /// ReadBiddingNewsToPagingByCondition
        /// </summary>
        /// <param name="iPaginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        IEnumerable<BiddingNewsVM> ReadBiddingNewsToPagingByCondition(IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null);

        /// <summary>
        /// GetSelectListBiddingPackage
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSelectListBiddingPackage();

        /// <summary>
        /// CheckContructionByProfileId
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        bool CheckContructionByProfileId(long? profileId);

        /// <summary>
        /// CheckContructionByProfileId
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        bool checkBiddingNewsSelected(long? biddingNewsId);
        bool checkBiddingNewsCompleted(long? biddingNewsId);
        /// <summary>
        /// ReadBiddingNewsById
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<BiddingNewsBidContractionDetailVM> ReadBiddingNewsById(long? biddingNewsId, UserProfile user);

        /// <summary>
        /// SearchBiddingNewsByCondition
        /// </summary>
        /// <param name="iPaginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        Task<IEnumerable<BiddingNewsVM>> SearchBiddingNewsByCondition(IPaginationInfo iPaginationInfo,
            UserProfile userProfile, string condition, string orderByStr = null);

        /// <summary>
        /// GetMaxTurnover2YearAbilityFinance
        /// </summary>
        /// <returns></returns>
        Task<long> GetMaxTurnover2YearAbilityFinance();

        Task<IList<BiddingNewsVM>> FilterBiddingNewsByCondition(bool isFilterValidNews, IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null);

        /// <summary>
        /// UpdateActiveStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        Task<bool> UpdateActiveStatus(long id, bool isActive);

        Task DeleteNews(long id);

        /// <summary>
        /// SaveBiddingNewsBookmark
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        Task<JsonResult> SaveBiddingNewsBookmark(long? biddingNewsId, UserProfile userProfile);

        /// <summary>
        /// CheckBiddingNewsProfileShowCondition
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        Task<BidShowContractDetailVM> CheckBiddingNewsProfileShowCondition(long? biddingNewsId, long? companyProfileId);
    }
}
