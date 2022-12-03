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
        Task<Guid> Create(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Update(T item);
        Task<bool> DeleteById(Guid id);
        Task<T> GetSingleByPredicate(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleByPredicateReadOnly(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetRangeByPredicate(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetRangeByPredicateReadOnly(Expression<Func<T, bool>> predicate);
    }
}
