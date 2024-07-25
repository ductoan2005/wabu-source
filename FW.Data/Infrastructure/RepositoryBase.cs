using FW.Common.Pagination;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;

namespace FW.Data.Infrastructure
{
    public abstract class RepositoryBase<T, TKey> where T : BaseEntity
    {
        #region Properties
        private FWDbContext dataContext;
        protected readonly DbSet<T> dbSet;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected FWDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            entity.DateInserted = entity.DateUpdated = DateTime.Now;
            dbSet.Add(entity);

        }

        public virtual void AddRange(List<T> entityList)
        {
            entityList.ForEach(x => {
                x.DateInserted = x.DateUpdated = DateTime.Now;
                x.IsDeleted = false;
            });
            dbSet.AddRange(entityList);
        }

        public virtual void Update(T entity, bool needToAttach = true)
        {
            entity.DateUpdated = DateTime.Now;
            if (needToAttach)
            {
                dbSet.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
                dataContext.Entry(entity).Property(p => p.DateInserted).IsModified = false;
            }
        }

        public virtual void BulkUpdate(List<T> entityList, bool needToAttach = true)
        {
            entityList.ForEach(x => {
                x.DateUpdated = DateTime.Now;
                if (needToAttach)
                {
                    dbSet.Attach(x);
                    dataContext.Entry(x).State = EntityState.Modified;
                    dataContext.Entry(x).Property(p => p.DateInserted).IsModified = false;
                }
            });
            
        }

        public virtual void Delete(T entity, bool needToAttach = true)
        {
            if (needToAttach)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(TKey id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).AsNoTracking().AsEnumerable();
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetWithFilterToPaginate(Expression<Func<T, bool>> where, PaginationInfo paginationInfo, string orderByStr)
        {
            var query = dbSet.Where(where);
            paginationInfo.TotalItems = query.Count();


            IEnumerable<T> resultList = query.OrderBy(orderByStr)
                                        .Skip(paginationInfo.ItemsToSkip)
                                        .Take(paginationInfo.ItemsPerPage).ToList();
            return resultList;
        }

        public virtual IEnumerable<T> GetAllToPaginate(PaginationInfo paginationInfo, string orderByStr)
        {
            var query = dbSet;
            paginationInfo.TotalItems = query.Count();

            IEnumerable<T> resultList = query.OrderBy(orderByStr);
            return resultList.Skip(paginationInfo.ItemsToSkip)
                             .Take(paginationInfo.ItemsPerPage).ToList();
        }

        public virtual void Detach(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual async Task<T> GetByIdAsync(TKey id) => await dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await dbSet.AsNoTracking().ToListAsync();

        public virtual async Task<T> GetWithConditionAsync(Expression<Func<T, bool>> where) => await dbSet.Where(where).AsNoTracking().FirstOrDefaultAsync();

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where) => await dbSet.Where(where).AsNoTracking().FirstOrDefaultAsync();
    }
}
