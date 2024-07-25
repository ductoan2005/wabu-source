using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using FW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingNewsRepository : IRepository<BiddingNews, long?>
    {
        #region Read

        /// <summary>
        /// ReadBiddingNewsToPagingByCondition
        /// </summary>
        /// <param name="paginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        IEnumerable<BiddingNewsVM> ReadBiddingNewsToPagingByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr);

        /// <summary>
        /// ReadBiddingNewsById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<BiddingNews> ReadBiddingNewsById(long? Id);

        /// <summary>
        /// ReadBiddingNewsById
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        bool CheckInvestorIsCompletedContractor(long? biddingNewsId);

        /// <summary>
        /// SearchBiddingNewsByCondition
        /// </summary>
        /// <param name="paginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        Task<IEnumerable<BiddingNewsVM>> SearchBiddingNewsByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr);

        /// <summary>
        /// GetMaxTurnover2YearAbilityFinance
        /// </summary>
        /// <returns></returns>
        Task<long> GetMaxTurnover2YearAbilityFinance();

        Task<IEnumerable<BiddingNewsVM>> FilterBiddingNewsByCondition(bool isFilterValidNews, PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr);

        #endregion

    }
}
