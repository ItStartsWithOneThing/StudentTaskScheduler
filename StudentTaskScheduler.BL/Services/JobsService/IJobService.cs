using StudentTaskScheduler.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.JobsService
{
    public interface IJobService
    {
        Task<IEnumerable<JobDTO>> GetRelevantJobs();
        Task<IEnumerable<JobDTO>> GetAllJobs();
        Task<bool> AssigneJob(Guid id);
        Task<bool> EndJob(Guid id);
    }
}
