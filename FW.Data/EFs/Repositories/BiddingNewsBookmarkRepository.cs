using FW.Common.Enum;
using FW.Common.Pagination;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels.BiddingNews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;

namespace FW.Data.EFs.Repositories
{
    public class BiddingNewsBookmarkRepository : RepositoryBase<BiddingNewsBookmark, long?>, IBiddingNewsBookmarkRepository
    {
        #region Ctor

        public BiddingNewsBookmarkRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        #endregion

        #region Methods

        public List<BiddingNewsBookmark> ReadBiddingNewsBookmarkToPagingByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr)
        {
            IQueryable<BiddingNewsBookmark> resultList = dbSet;
            IEnumerable<BiddingNewsBookmark> result;
            var currentDateTime = DateTime.UtcNow.AddHours(7);

            if (!string.IsNullOrEmpty(condition))
            {
                var biddingNewsSearchConditionVm = JsonConvert.DeserializeObject<BiddingNewsSearchConditionVM>(condition);

                //Search BidStartDate
                if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.FromDate))
                {
                    DateTime.TryParseExact(biddingNewsSearchConditionVm.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fromDate);
                    resultList = resultList.Where(x => DateTime.Compare(fromDate, x.BookmarkDate.Value) <= 0);
                }
                //Search BidCloseDate
                if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.ToDate))
                {
                    DateTime.TryParseExact(biddingNewsSearchConditionVm.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var toDate);
                    resultList = resultList.Where(x => DateTime.Compare(toDate, x.BookmarkDate.Value) >= 0);
                }
                //Search ContructionName
                if (biddingNewsSearchConditionVm.ContructionName != "Vui Lòng Chọn Công Trình")
                {
                    resultList = resultList.Where(x =>
                        x.BiddingNews.Construction.ConstructionName.StartsWith(biddingNewsSearchConditionVm.ContructionName));
                }
                //Search BiddingPackageType
                if (biddingNewsSearchConditionVm.BiddingPackageType != (byte)EBiddingPackageName.All)
                {
                    resultList = resultList.Where(x =>
                        x.BiddingNews.BiddingPackage.BiddingPackageType == biddingNewsSearchConditionVm.BiddingPackageType);
                }

                //Search with status condition
                //if (biddingNewsSearchConditionVm.StatusBidding.HasValue && (StatusBiddingNewsBookmark)biddingNewsSearchConditionVm.StatusBidding.GetHashCode() != StatusBiddingNewsBookmark.All)
                //{
                //    if ((StatusBiddingNewsBookmark)biddingNewsSearchConditionVm.StatusBidding.GetHashCode() == StatusBiddingNewsBookmark.Opening)
                //    {
                //        resultList = resultList.Where(x => currentDateTime < x.BiddingNews.BidCloseDate);
                //    }
                //    else
                //    {
                //        resultList = resultList.Where(x => currentDateTime >= x.BiddingNews.BidCloseDate);
                //    }
                //}

            }
            resultList = resultList.Where(x => x.UserId == userProfile.UserID && x.IsDeleted != true).AsNoTracking();

            result = !string.IsNullOrEmpty(orderByStr) ? resultList.OrderBy(orderByStr) : resultList.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

            paginationInfo.TotalItems = result.Count();

            return result.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();
        }

        #endregion
    }
}
