using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.Home;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class CompanyRepository : RepositoryBase<Company, long?>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory)
           : base(dbFactory)
        {
        }

        public IEnumerable<CompanyRatingVM> GetCompanyRating()
        {
            var query = (from c in dbSet
                         where c.IsDeleted != true
                         select new CompanyRatingVM
                         {
                             CompanyName = c.CompanyName,
                             DateInserted = c.DateInserted,
                             TotalBiddedNews = c.TotalBiddedNews,
                             ProjectImplemented = c.ProjectImplemented,
                             ProjectsComplete = c.ProjectsComplete,
                             Logo = c.Logo,
                             Link = c.Link,
                             OneStar = c.OneStar,
                             TwoStar = c.TwoStar,
                             ThreeStar = c.ThreeStar,
                             FourStar = c.FourStar,
                             FiveStar = c.FiveStar,
                         }).ToList();

            query.ForEach(f => f.RatingStar = CalculateRatingStar.GetRating(f.OneStar, f.TwoStar, f.ThreeStar, f.FourStar, f.FiveStar));
            query.ForEach(f => f.Link = !f.Link.Contains("https") && !f.Link.Contains("http") ? $"http://{f.Link}" : f.Link);

            query.OrderByDescending(o => o.RatingStar).Take(6);

            return query;
        }
        public IEnumerable<CompanyProfileLogoVM> ReadCompayLogoOnOver()
        {
            var query = (from c in dbSet.Where(x => x.IsOnOver == 1 && x.IsDeleted != true)
                         select new CompanyProfileLogoVM
                         {
                             CompanyName = c.CompanyName,
                             Logo = c.Logo,
                             IsOnOver = c.IsOnOver,
                             Address = c.CompanyAddress,
                             PhoneNumber = c.CompanyPhoneNumber,
                             RepresentativeName = c.RepresentativeName,
                             Introduction = c.Introduction,
                             Link = c.Link,
                             AdvertisingIsOn = c.AdvertisingIsOn,
                             AdvertisingBackgroundImage = c.AdvertisingBackgroundImage
                         }).ToList();

            return query;
        }

        /// <summary>
        /// ReadCompanyFromUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<Company> ReadCompanyFromUserId(long? userId)
            => dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.IsDeleted != true);

        public Task<string> GetCompanyNameFromUserId(long? userId)
            => dbSet.Where(x => x.UserId == userId && x.IsDeleted != true).Select(x => x.CompanyName).FirstOrDefaultAsync();
    }
}
