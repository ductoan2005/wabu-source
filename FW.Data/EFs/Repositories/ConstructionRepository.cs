using System;
using FW.Data.Infrastructure;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Collections.Generic;
using System.Linq;
using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FW.ViewModels;
using System.Globalization;

namespace FW.Data.EFs.Repositories
{
    public class ConstructionRepository : RepositoryBase<Construction, long?>, IConstructionRepository
    {
        public ConstructionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Construction> GetAllConstructionByUserId(long? userId)
        {
            IEnumerable<Construction> resultList = from c in dbSet
                         where c.UserId == userId && c.IsDeleted != true
                         select c;
            return resultList.ToList();
        }

        public IEnumerable<Construction> GetAllConstructionHasPagingByUserId(PaginationInfo paginationInfo, long? userId)
        {
            IEnumerable<Construction> resultList = dbSet.Where(c => c.IsDeleted != true && c.UserId == userId)
                .OrderByDescending(c => c.DateInserted)
                .ThenByDescending(c => c.DateUpdated);
            paginationInfo.TotalItems = resultList.Count();
            return resultList
                .Skip(paginationInfo.ItemsToSkip)
                .Take(paginationInfo.ItemsPerPage)
                .ToList();
        }

        public Construction GetImageConstructionById(long? id)
        {
            return dbSet.Where(p => p.IsDeleted != null && p.Id == id && p.IsDeleted != true).FirstOrDefault();
        }

        public Task<IEnumerable<Construction>> FilterConstructionByCondition(PaginationInfo paginationInfo,
           UserProfile userProfile, string condition, string orderByStr)
        {
            IEnumerable<Construction> result;
            IQueryable<Construction> query;

            if (string.IsNullOrEmpty(condition))
            {
                query = dbSet.Where(x => x.IsDeleted != true);

                query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

                paginationInfo.TotalItems = query.Count();

                result = query.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();

                return Task.FromResult(result);
            }

            var constructionConditionVm = JsonConvert.DeserializeObject<ConstructionVM>(condition);
            IQueryable<Construction> query2 = dbSet;            //Search Construction name
            if (!string.IsNullOrEmpty(constructionConditionVm.ConstructionName))
            {
                query2 = query2.Where(x => x.ConstructionName.Contains(constructionConditionVm.ConstructionName));
            }

            //Search Investor Name
            if (!string.IsNullOrEmpty(constructionConditionVm.InvestorName))
            {
                query2 = query2.Where(x => x.InvestorName.Contains(constructionConditionVm.InvestorName));
            }

            //Search Address Build
            if (!string.IsNullOrEmpty(constructionConditionVm.AddressBuild))
            {
                query2 = query2.Where(x => x.AddressBuild.Contains(constructionConditionVm.AddressBuild));
            }

            //Search Scale
            if (!string.IsNullOrEmpty(constructionConditionVm.StrScale))
            {
                var scale = int.Parse(constructionConditionVm.StrScale);
                query2 = query2.Where(x => x.Scale == scale);
            }

            //Search Basement
            if (!string.IsNullOrEmpty(constructionConditionVm.StrBasement))
            {
                var basement = int.Parse(constructionConditionVm.StrBasement);
                query2 = query2.Where(x => x.Basement == basement);
            }

            //Search AcreageBuild
            if (!string.IsNullOrEmpty(constructionConditionVm.StrAcreageBuild))
            {
                var acreageBuild = int.Parse(constructionConditionVm.StrAcreageBuild);
                query2 = query2.Where(x => x.AcreageBuild == acreageBuild);
            }

            //Search Email
            if (!string.IsNullOrEmpty(constructionConditionVm.ContactEmail))
            {
                query2 = query2.Where(x => x.ContactEmail.Contains(constructionConditionVm.ContactEmail));
            }

            //Search Email
            if (!string.IsNullOrEmpty(constructionConditionVm.ContactEmail))
            {
                query2 = query2.Where(x => x.ContactEmail.Contains(constructionConditionVm.ContactEmail));
            }

            //Search ContactName
            if (!string.IsNullOrEmpty(constructionConditionVm.ContactName))
            {
                query2 = query2.Where(x => x.ContactName.Contains(constructionConditionVm.ContactName));
            }

            //Search ContactPhoneNumber
            if (!string.IsNullOrEmpty(constructionConditionVm.ContactPhoneNumber))
            {
                query2 = query2.Where(x => x.ContactPhoneNumber.Contains(constructionConditionVm.ContactPhoneNumber));
            }

            //Search BuildingPermitDateTime
            if (!string.IsNullOrEmpty(constructionConditionVm.BuildingPermitDateTime))
            {
                DateTime.TryParseExact(constructionConditionVm.BuildingPermitDateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var buildingDate);
                query2 = query2.Where(x => DateTime.Compare(buildingDate, x.BuildingPermitDate.Value) == 0);
            }

            //Search ConstructionForm
            if (!string.IsNullOrEmpty(constructionConditionVm.StrConstructionForm))
            {
                var constructionForm = int.Parse(constructionConditionVm.StrConstructionForm);
                query2 = query2.Where(x => x.ConstructionForm == constructionConditionVm.ConstructionForm);
            }

            //Search AreaId
            if (!string.IsNullOrEmpty(constructionConditionVm.StrAreaId))
            {
                var areaId = int.Parse(constructionConditionVm.StrAreaId);
                query2 = query2.Where(x => x.AreaId == areaId);
            }

            query = query2.Where(x => x.IsDeleted != true);

            query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

            paginationInfo.TotalItems = query.Count();

            result = query.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();

            return Task.FromResult(result);
        }
    }
}
