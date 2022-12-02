using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
