using Microsoft.EntityFrameworkCore;
using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL.Repositories
{
    public class DbJobRepository : IDbJobRepository
    {
        private readonly EfCoreDbContext _dbContext;

        public DbJobRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EndJobAsync(Guid id, string status)
        {
            var dbJob = await _dbContext.Jobs.FirstOrDefaultAsync(x => x.Id == id);

            if(dbJob != null)
            {
                dbJob.JobStatus = status;

                var result = await _dbContext.SaveChangesAsync();

                if(result > 0)
                {
                    return true;
                }

                throw new Exception($"Something went wrong while changing job with id: {dbJob.Id}");
            }

            return false;
        }

        public async Task<IEnumerable<Job>> GetStudentJobsAsync(string firstName, string lastName)
        {
            var dbStudent = await _dbContext.Students
                .AsNoTracking()
                .Include(x => x.Jobs)
                .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);

            if(dbStudent != null)
            {
                return dbStudent.Jobs;
            }

            throw new Exception($"Can't find any student named {firstName} {lastName}");
        }
    }
}
