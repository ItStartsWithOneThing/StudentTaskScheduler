using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL.Repositories
{
    public interface IDbJobRepository
    {
        Task<bool> EndJobAsync(Guid id, string status);
        Task<IEnumerable<Job>> GetStudentJobsAsync(string firstName, string lastName);
    }
}
