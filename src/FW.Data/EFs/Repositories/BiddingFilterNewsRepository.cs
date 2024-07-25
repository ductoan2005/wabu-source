using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels.BiddingFilterNews;

namespace FW.Data.EFs.Repositories
{
    public class BiddingFilterNewsRepository : RepositoryBase<BiddingNews, long?>, IBiddingFilterNewsRepository
    {
        #region Ctor

        public BiddingFilterNewsRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// BiddingFilter
        /// </summary>
        /// <param name="DateInserted"></param>
        /// <param name="BidStartDate"></param>
        /// <param name="BidCloseDate"></param>
        /// <returns></returns>
        public Task<BiddingFilterNewsVM> BiddingFilter(DateTime? DateInserted, DateTime? BidStartDate, DateTime? BidCloseDate)
        {
            return null;
            //var dateNow = DateTime.Now.Date;
            //var query = (from q in (from c in dbSet
            //                        join b in DbContext.BiddingNews
            //                        on c.Id equals b.ConstructionId
            //                        select new
            //                        {
            //                            b.WorkContentId,
            //                            b.WorkContentOtherId,
            //                            c.ConstructionName,
            //                            c.InvestorName,
            //                            c.AreaId,
            //                            b.NumberBidder,
            //                            b.BidStartDate,
            //                            b.BidCloseDate,
            //                            b.BiddingPackageDescription
            //                        })
            //             join w in DbContext.WorkContents
            //             on q.WorkContentId equals w.Id into wlist
            //             from w in wlist.DefaultIfEmpty()


            //             join wo in DbContext.WorkContentOthers
            //             on q.WorkContentOtherId equals wo.Id into wolist
            //             from wo in wolist.DefaultIfEmpty()

            //             join a in DbContext.Areas
            //             on q.AreaId equals a.Id

            //             //where dateNow <= q.BidCloseDate
            //             select new BiddingFilterNewsVM
            //             {
            //                 WorkContentName = w.WorkContentName,
            //                 WorkContentOtherName = wo.WorkContentOtherName,
            //                 ConstructionName = q.ConstructionName,
            //                 InvestorName = q.InvestorName,
            //                 AreaName = a.AreaName,
            //                 NumberBidder = q.NumberBidder,
            //                 BidStartDate = q.BidStartDate,
            //                 BidCloseDate = q.BidCloseDate,
            //                 BiddingPackageDescription = q.BiddingPackageDescription
            //             }).Take(5).ToList();

            //return query;
        }

        #endregion
    }
}
