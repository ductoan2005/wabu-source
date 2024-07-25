using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FW.Common.Pagination;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels.EmailVM;

namespace FW.Data.EFs.Repositories
{
    public class CompanyProfileRepository : RepositoryBase<CompanyProfile, long?>, ICompanyProfileRepository
    {
        public CompanyProfileRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Read All Company Profiles Has Paging
        /// </summary>
        /// <param name="paginationInfo"></param>
        /// <returns></returns>
        public IEnumerable<CompanyProfile> ReadAllCompanyProfilesHasPaging(UserProfile userProfile, PaginationInfo paginationInfo)
        {
            var resultList = dbSet.Where(x => x.Company.UserId == userProfile.UserID && x.IsDeleted != true)
                    .OrderByDescending(c => c.DateInserted)
                    .ThenByDescending(c => c.DateUpdated);
            paginationInfo.TotalItems = resultList.Count();

            return resultList.Skip(paginationInfo.ItemsToSkip)
                  .Take(paginationInfo.ItemsPerPage)
                  .ToList();

        }

        /// <summary>
        /// Get all company profile has not bidding with biddingNewsId and userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompanyProfile>> GetAllCompanyProfilesHasNotBidding(long? userId, long? biddingNewsId)
        {
            IEnumerable<CompanyProfile> resultList = dbSet;
            //Check profiles of company
            var profileInCompany = (await DbContext.Company.FirstOrDefaultAsync(x => x.UserId == userId))?.CompanyProfiles.AsEnumerable();
            if (profileInCompany != null)
            {
                var profileInCompanyId = profileInCompany.Select(p => p.Id);
                //Check if one profiles of company has bidding
                var companyProfileIdHasBidding = DbContext.BiddingDetail.Any(x => x.BiddingNewsId == biddingNewsId && x.IsDeleted != true && profileInCompanyId.Contains(x.CompanyProfileId));
                resultList = companyProfileIdHasBidding ? null : profileInCompany;
            }

            return resultList;
        }

        public async Task<EmailVM> GetUserRole3ByCompanyProfileId(long? companyProfileId, long? biddingNewsId)
        {
            var query = await DbContext.BiddingDetail
                                 .Include(i => i.BiddingNews.User)
                                 .Include(i=>i.BiddingNews.BiddingPackage)
                                 .Include(i => i.CompanyProfile.Company.User)
                                 .Where(w => w.CompanyProfileId == companyProfileId
                                            && w.BiddingNewsId == biddingNewsId
                                            && w.IsDeleted != true)
                                .Select(s => new EmailVM()
                                {
                                    BiddingNewsId = s.BiddingNews.Id,
                                    BiddingPackageName = s.BiddingNews.BiddingPackage.BiddingPackageName,
                                    BiddingPackageId = s.BiddingNews.BiddingPackage.Id,
                                    Email = s.CompanyProfile.Company.User.Email,
                                    CompanyName = s.CompanyProfile.Company.CompanyName,
                                    FullName = s.BiddingNews.Construction.ContactName,
                                    ConstructionName = s.BiddingNews.Construction.ConstructionName,
                                    contructionid = s.BiddingNews.Construction.Id.Value,
                                    InvestorName = s.BiddingNews.Construction.InvestorName,
                                    InvestorEmail = s.BiddingNews.Construction.ContactEmail,
                                    InvestorPhone = s.BiddingNews.Construction.ContactPhoneNumber,
                                    UserIdRole3 = s.CompanyProfile.Company.User.Id,
                                    UserIdRole2 = s.BiddingNews.User.Id,
                                }).FirstOrDefaultAsync();

            return query;
        }
        
        
    }
}
