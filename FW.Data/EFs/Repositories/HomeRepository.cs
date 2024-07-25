using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using FW.ViewModels;
using System.Data.Entity;

namespace FW.Data.EFs.Repositories
{
    public class HomeRepository : RepositoryBase<Construction, long?>, IHomeRepository
    {
        public HomeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsBest()
        {
            // News vừa được tạo trong vòng 7 ngày
            var oneWeekAgo = DateTime.Now.AddDays(-7).Date;
            var dateNow = DateTime.Now.Date;
            var query = (from q in (from c in dbSet
                                    join b in DbContext.BiddingNews
                                    on c.Id equals b.ConstructionId
                                    select new
                                    {
                                        b.Id,
                                        b.Construction.Image1FilePath,
                                        c.ConstructionName,
                                        c.InvestorName,
                                        c.AreaId,
                                        b.BiddingPackage.BiddingPackageName,
                                        b.NumberBidder,
                                        b.NumberBidded,
                                        b.DateUpdated,
                                        b.BidCloseDate,
                                        b.BiddingPackageDescription,
                                        b.IsActived,
                                        b.NewsApprovalDate
                                    })
                         join a in DbContext.Areas
                         on q.AreaId equals a.Id

                         where q.NewsApprovalDate.Value >= oneWeekAgo && q.IsActived && dateNow <= q.BidCloseDate
                         select new BiddingNewsCommonVM
                         {
                             BiddingNewsId = q.Id,
                             Image = q.Image1FilePath,
                             ConstructionName = q.ConstructionName.ToUpper(),
                             BiddingPackageName = q.BiddingPackageName,
                             InvestorName = q.InvestorName,
                             AreaName = a.AreaName,
                             NumberBidder = q.NumberBidder,
                             NumberBidded = q.NumberBidded,
                             DateUpdated = q.DateUpdated,
                             BidCloseDate = q.BidCloseDate,
                             BiddingPackageDescription = q.BiddingPackageDescription,
                             NewsApprovalDate = q.NewsApprovalDate
                         }).Take(6).ToList();

            return query;
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsNewest()
        {
            // News vừa được tạo trong vòng 7 ngày
            var oneWeekAgo = DateTime.Now.AddDays(-7).Date;
            var dateNow = DateTime.Now;

           var query = (from q in (from c in dbSet
                                    join b in DbContext.BiddingNews
                                    on c.Id equals b.ConstructionId
                                    select new
                                    {
                                        b.Id,
                                        b.Construction.Image1FilePath,
                                        c.ConstructionName,
                                        c.InvestorName,
                                        c.AreaId,
                                        b.BiddingPackage.BiddingPackageName,
                                        b.NewsApprovalDate,
                                        b.NumberBidded,
                                        b.DateUpdated,
                                        b.BidCloseDate,
                                        b.BiddingPackageDescription,
                                        b.IsActived
                                    })

                         join a in DbContext.Areas
                         on q.AreaId equals a.Id

                         where q.NewsApprovalDate.Value >= oneWeekAgo && q.IsActived && dateNow <= q.BidCloseDate
                         select new BiddingNewsCommonVM
                         {
                             BiddingNewsId = q.Id,
                             Image = q.Image1FilePath,
                             ConstructionName = q.ConstructionName.ToUpper(),
                             BiddingPackageName = q.BiddingPackageName,
                             InvestorName = q.InvestorName,
                             AreaName = a.AreaName,
                             //NumberBidder = q.NumberBidder,
                             NumberBidded = q.NumberBidded,
                             DateUpdated = q.DateUpdated,
                             BidCloseDate = q.BidCloseDate,
                             BiddingPackageDescription = q.BiddingPackageDescription,
                             NewsApprovalDate = q.NewsApprovalDate
                         }).Take(20).ToList();

            return query;
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsInterest()
        {
            // News vừa được tạo trong vòng 7 ngày
            var oneWeekAgo = DateTime.Now.AddDays(-7).Date;
            // News có được 5 nhà thầu trở lên(*)
            var dateNow = DateTime.Now.Date;
            var query = (from q in (from c in dbSet
                                    join b in DbContext.BiddingNews
                                    on c.Id equals b.ConstructionId
                                    select new
                                    {
                                        b.Id,
                                        b.Construction.Image1FilePath,
                                        c.ConstructionName,
                                        c.InvestorName,
                                        c.AreaId,
                                        b.BiddingPackage.BiddingPackageName,
                                        b.NumberBidder,
                                        b.NumberBidded,
                                        b.DateUpdated,
                                        b.BidCloseDate,
                                        b.BiddingPackageDescription,
                                        b.IsActived,
                                        b.NewsApprovalDate,
                                    })
                         join a in DbContext.Areas
                         on q.AreaId equals a.Id

                         where q.NumberBidded >= 5 && q.NewsApprovalDate.Value >= oneWeekAgo && q.IsActived && dateNow <= q.BidCloseDate
                         select new BiddingNewsCommonVM
                         {
                             BiddingNewsId = q.Id,
                             Image = q.Image1FilePath,
                             ConstructionName = q.ConstructionName.ToUpper(),
                             BiddingPackageName = q.BiddingPackageName,
                             InvestorName = q.InvestorName,
                             AreaName = a.AreaName,
                             NumberBidder = q.NumberBidder,
                             NumberBidded = q.NumberBidded,
                             DateUpdated = q.DateUpdated,
                             BidCloseDate = q.BidCloseDate,
                             BiddingPackageDescription = q.BiddingPackageDescription,
                             NewsApprovalDate = q.NewsApprovalDate
                         }).Take(20).ToList();

            return query;
        }



        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsSuggest()
        {
            // News vừa được tạo trong vòng 7 ngày
            var oneWeekAgo = DateTime.Now.AddDays(-7).Date;
            // News có 20% số lượng đấu thấu (*)
            var dateNow = DateTime.Now.Date;
            var query = (from q in (from c in dbSet
                                    join b in DbContext.BiddingNews
                                    on c.Id equals b.ConstructionId
                                    select new
                                    {
                                        b.Id,
                                        c.ConstructionName,
                                        c.InvestorName,
                                        c.AreaId,
                                        b.BiddingPackage.BiddingPackageName,
                                        b.NumberBidder,
                                        b.NumberBidded,
                                        b.DateUpdated,
                                        b.BidCloseDate,
                                        b.BiddingPackageDescription,
                                        b.IsActived,
                                        b.NewsApprovalDate
                                    })

                         join a in DbContext.Areas
                         on q.AreaId equals a.Id

                         where q.NumberBidded <= (q.NumberBidder * 0.2) && q.NewsApprovalDate.Value >= oneWeekAgo && q.IsActived && dateNow <= q.BidCloseDate
                         select new BiddingNewsCommonVM
                         {
                             BiddingNewsId = q.Id,
                             ConstructionName = q.ConstructionName.ToUpper(),
                             BiddingPackageName = q.BiddingPackageName,
                             InvestorName = q.InvestorName,
                             AreaName = a.AreaName,
                             NumberBidder = q.NumberBidder,
                             NumberBidded = q.NumberBidded,
                             DateUpdated = q.DateUpdated,
                             BidCloseDate = q.BidCloseDate,
                             BiddingPackageDescription = q.BiddingPackageDescription,
                             NewsApprovalDate = q.NewsApprovalDate
                         }).Take(20).ToList();

            return query;
        }


    }
}
