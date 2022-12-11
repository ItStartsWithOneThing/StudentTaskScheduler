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
        Task<IEnumerable<JobDTO>> GetRelevantJobsAsync();
        Task<IEnumerable<JobFullInfoDTO>> GetRelevantJobsWithFullInfoAsync();
        Task<IEnumerable<JobFullInfoDTO>> GetAllJobsWithFullInfoAsync();
        Task<IEnumerable<JobDTO>> GetStudentJobsAsync(string firstName, string lastName);
        Task<Guid> CreateJobAsync(JobFullInfoDTO job);
        Task<bool> EndJobAsync(Guid id);
    }
}
