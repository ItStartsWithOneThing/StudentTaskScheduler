using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL.Repositories
{
    public interface IDbGenericRepository<T> where T: BaseEntity, new()
    {
        Task<Guid> CreateAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<T> GetSingleByPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleByPredicateReadOnlyAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetRangeByPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetRangeByPredicateReadOnlyAsync(Expression<Func<T, bool>> predicate);
    }
}
