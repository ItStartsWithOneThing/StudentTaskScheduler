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
        Task<IEnumerable<JobFullInfoDTO>> GetRelevantJobsWithFullInfo();
        Task<IEnumerable<JobFullInfoDTO>> GetAllJobsWithFullInfo();
        Task<IEnumerable<JobDTO>> GetStudentJobs(string firstName, string lastName);
        Task<Guid> CreateJob(JobFullInfoDTO job);
        Task<bool> EndJob(Guid id);
    }
}
