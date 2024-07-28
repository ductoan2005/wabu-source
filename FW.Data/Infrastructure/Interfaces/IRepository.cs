using FW.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FW.Data.Infrastructure.Interfaces
{
    public interface IRepository<T, TKey> where T : class
    {
        void Add(T entity);

        void AddRange(List<T> entityList);

        void Update(T entity, bool needToAttach = true);

        void BulkUpdate(List<T> entityList, bool needToAttach = true);

        void Delete(T entity, bool needToAttach = true);

        void Delete(Expression<Func<T, bool>> where);

        T GetById(TKey id);

        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        IEnumerable<T> GetWithFilterToPaginate(Expression<Func<T, bool>> where, PaginationInfo paginationInfo, string orderByStr);

        IEnumerable<T> GetAllToPaginate(PaginationInfo paginationInfo, string orderByStr);

        Task<T> GetByIdAsync(TKey id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetWithConditionAsync(Expression<Func<T, bool>> where);

        Task<T> GetAsync(Expression<Func<T, bool>> where);

        Task<IEnumerable<T>> GetListWithConditionsAsync(Expression<Func<T, bool>> where);
    }
}
