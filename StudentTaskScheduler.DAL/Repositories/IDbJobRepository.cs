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
        Task<bool> EndJob(Guid id, string status);
        Task<IEnumerable<Job>> GetStudentJobs(string firstName, string lastName);
    }
}
