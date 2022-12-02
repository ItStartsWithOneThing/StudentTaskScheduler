using StudentTaskScheduler.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.JobsService
{
    public class JobService : IJobService
    {
        public Task<bool> AssigneJob(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EndJob(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobDTO>> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobDTO>> GetRelevantJobs()
        {
            throw new NotImplementedException();
        }
    }
}
