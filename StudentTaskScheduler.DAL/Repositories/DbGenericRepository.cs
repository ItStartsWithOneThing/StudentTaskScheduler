using Microsoft.EntityFrameworkCore;
using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL.Repositories
{
    public class DbGenericRepository<T> : IDbGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly EfCoreDbContext _dbContext;
        private DbSet<T> _dbSet;

        public DbGenericRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public async Task<Guid> Create(T item)
        {
            item.Id = Guid.NewGuid();

            await _dbSet.AddAsync(item);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return item.Id;
            }

            throw new Exception($"Error occurred while creating new item with id: {item.Id} in Database");
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var dbItem = await _dbSet.FindAsync(id);

            if(dbItem == null)
            {
                return false;
            }

            _dbContext.Entry(dbItem).State = EntityState.Deleted;

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            throw new Exception($"Error occurred while deleting item with id: {id}");
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(T item)
        {
            var dbItem = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);

            if(dbItem == null)
            {
                throw new Exception($"Item with id: {item.Id} does not exist");
            }

            _dbContext.Entry(item).State = EntityState.Modified;

            var result = await _dbContext.SaveChangesAsync();

            if(result > 0)
            {
                return true;
            }

            throw new Exception($"Error occurred while modifying item with id: {item.Id}");
        }

        public async Task<T> GetSingleByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleByPredicateReadOnly(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<T>> GetRangeByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetRangeByPredicateReadOnly(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
