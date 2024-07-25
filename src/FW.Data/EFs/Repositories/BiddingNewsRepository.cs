using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class BiddingNewsRepository : RepositoryBase<BiddingNews, long?>, IBiddingNewsRepository
    {
        #region Ctor

        public BiddingNewsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// ReadBiddingNewsToPagingByCondition
        /// </summary>
        /// <param name="paginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public IEnumerable<BiddingNewsVM> ReadBiddingNewsToPagingByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr)
        {
            IQueryable<BiddingNewsVM> resultList = Enumerable.Empty<BiddingNewsVM>().AsQueryable();

            IEnumerable<BiddingNewsVM> result;

            //search as contrator
            if (userProfile.Authority == 3)
            {
                resultList = (from bn in dbSet
                              join bp in DbContext.BiddingPackages on bn.BiddingPackageId equals bp.Id into bnp
                              from bp in bnp.DefaultIfEmpty()
                              join bd in DbContext.BiddingDetail on bn.Id equals bd.BiddingNewsId
                              join ctr in DbContext.Contructions on bn.ConstructionId equals ctr.Id
                              where bd.CompanyProfile.Company.UserId == userProfile.UserID && bn.IsDeleted != true
                              && bn.BidCloseDate >= DateTime.Now && bn.IsActived
                              select new BiddingNewsVM
                              {
                                  Id = bn.Id,
                                  IsActived = bn.IsActived,
                                  ContructionName = ctr.ConstructionName.ToUpper(),
                                  BiddingPackageName = bp.BiddingPackageName,
                                  BiddingPackageType = bp.BiddingPackageType,
                                  StatusBiddingNews = bn.StatusBiddingNews,
                                  InvestorName = ctr.InvestorName,
                                  BidStartDate = bn.BidStartDate,
                                  BidCloseDate = bn.BidCloseDate,
                                  NumberBidded = bn.NumberBidded,
                                  Authority = userProfile.Authority,
                                  DateInserted = bn.DateInserted,
                                  DateUpdated = bn.DateUpdated
                              }).Distinct();
            }
            //search as investor
            else if (userProfile.Authority == 2)
            {
                resultList = (from bn in dbSet
                              join bp in DbContext.BiddingPackages on bn.BiddingPackageId equals bp.Id into bnp
                              from bp in bnp.DefaultIfEmpty()
                              join ctr in DbContext.Contructions on bn.ConstructionId equals ctr.Id
                              where bn.UserId == userProfile.UserID && bn.IsDeleted != true //&& bn.BidCloseDate >= DateTime.Now && bn.IsActived
                              select new BiddingNewsVM
                              {
                                  Id = bn.Id,
                                  IsActived = bn.IsActived,
                                  ContructionName = ctr.ConstructionName.ToUpper(),
                                  BiddingPackageName = bp.BiddingPackageName,
                                  BiddingPackageType = bp.BiddingPackageType,
                                  StatusBiddingNews = bn.StatusBiddingNews,
                                  InvestorName = ctr.InvestorName,
                                  BidStartDate = bn.BidStartDate,
                                  BidCloseDate = bn.BidCloseDate,
                                  NumberBidded = bn.NumberBidded,
                                  Authority = userProfile.Authority,
                                  DateInserted = bn.DateInserted,
                                  DateUpdated = bn.DateUpdated
                              }).Distinct();
            }
            else
            {
                resultList = (from bn in dbSet
                              join bp in DbContext.BiddingPackages on bn.BiddingPackageId equals bp.Id into bnp
                              from bp in bnp.DefaultIfEmpty()
                              join ctr in DbContext.Contructions on bn.ConstructionId equals ctr.Id
                              where bn.IsDeleted != true && bn.BidCloseDate >= DateTime.Now && bn.IsActived
                              select new BiddingNewsVM
                              {
                                  Id = bn.Id,
                                  ContructionName = ctr.ConstructionName.ToUpper(),
                                  BiddingPackageName = bp.BiddingPackageName,
                                  BiddingPackageType = bp.BiddingPackageType,
                                  StatusBiddingNews = bn.StatusBiddingNews,
                                  InvestorName = ctr.InvestorName,
                                  BidStartDate = bn.BidStartDate,
                                  BidCloseDate = bn.BidCloseDate,
                                  NumberBidder = bn.NumberBidder,
                                  Authority = userProfile.Authority,
                                  DateInserted = bn.DateInserted,
                                  DateUpdated = bn.DateUpdated
                              }).Distinct();
            }

            //Search with condition
            if (!string.IsNullOrWhiteSpace(condition))
            {
                var biddingNewsSearchConditionVm =
                    JsonConvert.DeserializeObject<BiddingNewsSearchConditionVM>(condition);
                //Search BidStartDate
                if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.FromDate))
                {
                    DateTime.TryParseExact(biddingNewsSearchConditionVm.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fromDate);
                    resultList = resultList.Where(x => DateTime.Compare(fromDate, x.BidStartDate.Value) <= 0);
                }
                //Search BidCloseDate
                if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.ToDate))
                {
                    DateTime.TryParseExact(biddingNewsSearchConditionVm.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var toDate);
                    resultList = resultList.Where(x => DateTime.Compare(toDate, x.BidStartDate.Value) >= 0);
                }
                //Search ContructionName
                if (biddingNewsSearchConditionVm.ContructionName != "Vui Lòng Chọn Công Trình")
                {
                    resultList = resultList.Where(x =>
                        x.ContructionName.StartsWith(biddingNewsSearchConditionVm.ContructionName));
                }
                //Search BiddingPackageType
                if (biddingNewsSearchConditionVm.BiddingPackageType != (byte)EBiddingPackageName.All)
                {
                    resultList = resultList.Where(x =>
                        x.BiddingPackage.BiddingPackageType == biddingNewsSearchConditionVm.BiddingPackageType);
                }

                //Search with status condition
                if (biddingNewsSearchConditionVm.StatusBidding.HasValue && (StatusBidding)biddingNewsSearchConditionVm.StatusBidding.GetHashCode() != StatusBidding.All)
                {
                    resultList = resultList.Where(x => x.StatusBiddingNews == biddingNewsSearchConditionVm.StatusBidding);
                }
            }

            resultList = resultList.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

            result = resultList.Skip(paginationInfo.ItemsToSkip)
                       .Take(paginationInfo.ItemsPerPage)
                       .ToList();

            paginationInfo.TotalItems = resultList.Count();

            return result;

        }

        /// <summary>
        /// ReadBiddingNewsById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BiddingNews> ReadBiddingNewsById(long? Id)
        {
            return await dbSet.FirstOrDefaultAsync(x => Id == x.Id && x.IsDeleted != true);
        }

        /// <summary>
        /// ReadBiddingNewsById
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        public bool CheckInvestorIsCompletedContractor(long? biddingNewsId)
        {
            return dbSet.Any(x => biddingNewsId == x.Id && x.IsDeleted != true && x.StatusBiddingNews == 3);
        }

        /// <summary>
        /// SearchBiddingNewsByCondition
        /// </summary>
        /// <param name="paginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public Task<IEnumerable<BiddingNewsVM>> SearchBiddingNewsByCondition(PaginationInfo paginationInfo,
            UserProfile userProfile, string condition, string orderByStr)
        {
            IEnumerable<BiddingNewsVM> result;
            IQueryable<BiddingNewsVM> query;

            if (string.IsNullOrEmpty(condition))
            {
                query = dbSet.Where(x => x.IsDeleted != true && x.BidCloseDate >= DateTime.Now && x.IsActived)
                    .Join(DbContext.Contructions.Where(x => x.IsDeleted != true),
                    x => x.ConstructionId,
                    y => y.Id,
                    (x, y) => new { BiddingNews = x, Contructions = y })
                    .Select(x => new BiddingNewsVM
                    {
                        Id = x.BiddingNews.Id,
                        NumberBidder = x.BiddingNews.NumberBidder,
                        NumberBidded = x.BiddingNews.NumberBidded,
                        BidStartDate = x.BiddingNews.BidStartDate,
                        BidCloseDate = x.BiddingNews.BidCloseDate,
                        DateInserted = x.BiddingNews.DateInserted,
                        DateUpdated = x.BiddingNews.DateUpdated,
                        NewsApprovalDate = x.BiddingNews.NewsApprovalDate,
                        ConstructionVM = new ConstructionVM
                        {
                            ConstructionName = x.Contructions.ConstructionName.ToUpper(),
                            InvestorName = x.Contructions.InvestorName,
                            Image1FilePath = x.Contructions.Image1FilePath
                        },
                        BiddingPackageVM = new BiddingPackageVM
                        {
                            BiddingPackageName = x.BiddingNews.BiddingPackage.BiddingPackageName
                        }
                    }).AsNoTracking();

                query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

                result = query.Skip(paginationInfo.ItemsToSkip)
                   .Take(paginationInfo.ItemsPerPage)
                   .ToList();

                paginationInfo.TotalItems = query.Count();

                return Task.FromResult(result);
            }

            var biddingNewsSearchConditionVm = JsonConvert.DeserializeObject<BiddingNewsSearchConditionVM>(condition);
            IQueryable<BiddingNews> query2 = dbSet;
            //Search with area
            if (biddingNewsSearchConditionVm.AreaId != -1)
            {
                query2 = query2.Where(x => x.Construction.Area.Id == biddingNewsSearchConditionVm.AreaId);
            }

            //Search with status bidding news condition
            if (biddingNewsSearchConditionVm.StatusBidding.HasValue && (StatusBidding)biddingNewsSearchConditionVm.StatusBidding.GetHashCode() != StatusBidding.All)
            {
                query2 = query2.Where(x => x.StatusBiddingNews == biddingNewsSearchConditionVm.StatusBidding);
            }

            // Search with NumberYearActivityAbilityExp
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.NumberYearActivityAbilityExp))
            {
                var range = biddingNewsSearchConditionVm.NumberYearActivityAbilityExp.Split(';');
                var from = int.Parse(range.First());
                var to = int.Parse(range[1]);
                query2 = query2.Where(x => x.NumberYearActivityAbilityExp >= from && x.NumberYearActivityAbilityExp <= to);
            }

            // Search with NumberSimilarContractAbilityExp
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.NumberSimilarContractAbilityExp))
            {
                var range = biddingNewsSearchConditionVm.NumberSimilarContractAbilityExp.Split(';');
                var from = int.Parse(range.First());
                var to = int.Parse(range[1]);
                query2 = query2.Where(x => x.NumberSimilarContractAbilityExp >= from && x.NumberSimilarContractAbilityExp <= to);
            }

            // Search with Turnover2YearAbilityFinance
            //if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.Turnover2YearAbilityFinance))
            //{
            //    var range = biddingNewsSearchConditionVm.NumberSimilarContractAbilityExp.Split(';');
            //    var from = int.Parse(range.First());
            //    var to = int.Parse(range[1]);
            //    query = query.Where(x => x.Turnover2YearAbilityFinance >= from && x.Turnover2YearAbilityFinance <= to);
            //}

            //Search BidStartDate
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.FromDate))
            {
                DateTime.TryParseExact(biddingNewsSearchConditionVm.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var fromDate);
                query2 = query2.Where(x => DateTime.Compare(fromDate, x.BidStartDate.Value) == 0);
            }

            //Search Job position
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.JobPosition))
            {
                var biddingNewsId = DbContext.BiddingNewsAbilityHRs.Where(x => x.JobPosition.ToLower().Contains(biddingNewsSearchConditionVm.JobPosition.ToLower()))
                    .Select(x => x.BiddingNewsId.Value);
                query2 = query2.Where(x => biddingNewsId.Contains(x.Id.Value));
            }

            //Textbox Search
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.TextSearch))
            {
                if (decimal.TryParse(biddingNewsSearchConditionVm.TextSearch, out var price))
                {
                    var biddingNewsId = DbContext.BiddingDetail.Where(x => x.Price >= price).Select(x => x.BiddingNewsId.Value);
                    query2 = query2.Where(x => biddingNewsId.Contains(x.Id.Value));
                }
                else
                {
                    var keyword = biddingNewsSearchConditionVm.TextSearch.ToLower();
                    query2 = query2.Where(x => x.BiddingPackage.BiddingPackageName.ToLower().Contains(keyword) || x.Construction.ConstructionName.ToLower().Contains(keyword));
                }
            }

            //Search with biddingPackageName
            if (biddingNewsSearchConditionVm.BiddingPackageId != null &&
                biddingNewsSearchConditionVm.BiddingPackageId != 0)
            {
                query2 = query2.Where(x => x.BiddingPackage.BiddingPackageType == biddingNewsSearchConditionVm.BiddingPackageId);
            }

            query = query2.Where(x => x.IsDeleted != true && x.BidCloseDate >= DateTime.Now && x.IsActived)
                    .Join(DbContext.Contructions.Where(x => x.IsDeleted != true),
                    x => x.ConstructionId,
                    y => y.Id,
                    (x, y) => new { BiddingNews = x, Contructions = y })
                    .Select(x => new BiddingNewsVM
                    {
                        Id = x.BiddingNews.Id,
                        NumberBidder = x.BiddingNews.NumberBidder,
                        NumberBidded = x.BiddingNews.NumberBidded,
                        BidStartDate = x.BiddingNews.BidStartDate,
                        BidCloseDate = x.BiddingNews.BidCloseDate,
                        DateInserted = x.BiddingNews.DateInserted,
                        DateUpdated = x.BiddingNews.DateUpdated,
                        NewsApprovalDate = x.BiddingNews.NewsApprovalDate,
                        ConstructionVM = new ConstructionVM
                        {
                            ConstructionName = x.Contructions.ConstructionName.ToUpper(),
                            InvestorName = x.Contructions.InvestorName,
                            Image1FilePath = x.Contructions.Image1FilePath
                        },
                        BiddingPackageVM = new BiddingPackageVM
                        {
                            BiddingPackageName = x.BiddingNews.BiddingPackage.BiddingPackageName
                        }
                    }).AsNoTracking();

            query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

            result = query.Skip(paginationInfo.ItemsToSkip)
               .Take(paginationInfo.ItemsPerPage)
               .ToList();

            paginationInfo.TotalItems = query.Count();

            return Task.FromResult(result);
        }

        /// <summary>
        /// GetMaxTurnover2YearAbilityFinance
        /// </summary>
        /// <returns></returns>
        public Task<long> GetMaxTurnover2YearAbilityFinance()
            => dbSet.MaxAsync(x => x.Turnover2YearAbilityFinance);

        /// <summary>
        /// FilterBiddingNewsByCondition
        /// </summary>
        /// <param name="paginationInfo"></param>
        /// <param name="userProfile"></param>
        /// <param name="condition"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public Task<IEnumerable<BiddingNewsVM>> FilterBiddingNewsByCondition(bool isValidNews,
                                                                             PaginationInfo paginationInfo,
                                                                             UserProfile userProfile, string condition,
                                                                             string orderByStr)
        {
            IEnumerable<BiddingNewsVM> result;
            IQueryable<BiddingNewsVM> query;

            if (string.IsNullOrEmpty(condition))
            {
                query = dbSet.Where(x => x.IsDeleted != true)
                     .Join(DbContext.Contructions.Where(x => x.IsDeleted != true),
                    x => x.ConstructionId,
                    y => y.Id,
                    (x, y) => new { BiddingNews = x, Contructions = y })
                    .Select(x => new BiddingNewsVM
                    {
                        Id = x.BiddingNews.Id,
                        DateInserted = x.BiddingNews.DateInserted,
                        DateUpdated = x.BiddingNews.DateUpdated,
                        BidCloseDate = x.BiddingNews.BidCloseDate,
                        IsActived = x.BiddingNews.IsActived,
                        ConstructionVM = new ConstructionVM
                        {
                            ConstructionName = x.Contructions.ConstructionName.ToUpper(),
                            InvestorName = x.Contructions.InvestorName
                        },
                        BiddingPackageVM = new BiddingPackageVM
                        {
                            BiddingPackageName = x.BiddingNews.BiddingPackage.BiddingPackageName
                        }
                    });

                query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

                paginationInfo.TotalItems = query.Count();

                result = query.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();

                return Task.FromResult(result);
            }

            var biddingNewsSearchConditionVm = JsonConvert.DeserializeObject<FilterBiddingNewsSearchConditionVM>(condition);
            IQueryable<BiddingNews> query2 = dbSet;
            //Search Created Date
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.CreatedDate))
            {
                DateTime.TryParseExact(biddingNewsSearchConditionVm.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var createDate);
                query2 = query2.Where(x => DateTime.Compare(createDate, x.DateInserted.Value) <= 0);
            }

            //Search BidStartDate
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.FromDate))
            {
                DateTime.TryParseExact(biddingNewsSearchConditionVm.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var fromDate);
                query2 = query2.Where(x => DateTime.Compare(fromDate, x.BidStartDate.Value) <= 0);
            }

            //Search BidCloseDate
            if (!string.IsNullOrEmpty(biddingNewsSearchConditionVm.ToDate))
            {
                DateTime.TryParseExact(biddingNewsSearchConditionVm.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var toDate);
                query2 = query2.Where(x => DateTime.Compare(toDate, x.BidCloseDate.Value) >= 0);
            }

            //Search with status is active
            query2 = query2.Where(x => x.IsActived == biddingNewsSearchConditionVm.StatusActive);

            //Search with status bidding news condition
            if (biddingNewsSearchConditionVm.StatusBidding.HasValue && (StatusBidding)biddingNewsSearchConditionVm.StatusBidding.GetHashCode() != StatusBidding.All)
            {
                query2 = query2.Where(x => x.StatusBiddingNews == biddingNewsSearchConditionVm.StatusBidding);
            }

            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now;
            query = query2.Where(x =>
                x.IsDeleted != true
                && ((isValidNews && startDate <= x.DateInserted && x.DateInserted <= endDate)
                || (!isValidNews && startDate > x.DateInserted)))
                .Join(DbContext.Contructions.Where(x => x.IsDeleted != true),
                    x => x.ConstructionId,
                    y => y.Id,
                    (x, y) => new { BiddingNews = x, Contructions = y })
                    .Select(x => new BiddingNewsVM
                    {
                        Id = x.BiddingNews.Id,
                        DateInserted = x.BiddingNews.DateInserted,
                        DateUpdated = x.BiddingNews.DateUpdated,
                        BidCloseDate = x.BiddingNews.BidCloseDate,
                        IsActived = x.BiddingNews.IsActived,
                        ConstructionVM = new ConstructionVM
                        {
                            ConstructionName = x.Contructions.ConstructionName.ToUpper(),
                            InvestorName = x.Contructions.InvestorName
                        },
                        BiddingPackageVM = new BiddingPackageVM
                        {
                            BiddingPackageName = x.BiddingNews.BiddingPackage.BiddingPackageName
                        }
                    })
                .AsNoTracking();

            query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

            paginationInfo.TotalItems = query.Count();

            result = query.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();

            return Task.FromResult(result);
        }

        #endregion Methods
    }
}