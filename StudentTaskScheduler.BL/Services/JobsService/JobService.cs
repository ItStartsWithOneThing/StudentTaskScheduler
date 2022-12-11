using AutoMapper;
using StudentTaskScheduler.BL.DTOs;
using StudentTaskScheduler.DAL.Entities;
using StudentTaskScheduler.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.JobsService
{
    public class JobService : IJobService
    {
        private readonly IDbGenericRepository<Job> _genericJobRepository;
        private readonly IDbGenericRepository<Student> _genericStudentRepository;
        private readonly IDbJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobService(
            IDbGenericRepository<Job> genericRepository,
            IDbGenericRepository<Student> genericStudentRepository,
            IDbJobRepository jobRepository,
            IMapper mapper)
        {
            _genericJobRepository = genericRepository;
            _genericStudentRepository = genericStudentRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateJobAsync(JobFullInfoDTO job)
        {
            if (job == null)
            {
                throw new Exception("You are trying to create an empty object");
            }

            var dbStudent = await _genericStudentRepository.GetByIdAsync(job.AssignedToId);

            if(dbStudent == null)
            {
                return Guid.Empty;
            }

            job.AssignedTo = dbStudent.FirstName + " " + dbStudent.LastName;
            job.StartDate = DateTime.Now;
            job.JobStatus = JobStatuses.InProgress;

            var dbJob = _mapper.Map<Job>(job);

            return await _genericJobRepository.CreateAsync(dbJob);
        }

        public async Task<bool> EndJobAsync(Guid id)
        {
            return await _jobRepository.EndJobAsync(id, JobStatuses.Done);
        }

        public async Task<IEnumerable<JobFullInfoDTO>> GetAllJobsWithFullInfoAsync()
        {
            var dbJobs = await _genericJobRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<JobFullInfoDTO>>(dbJobs);

            return result;
        }

        public async Task<IEnumerable<JobDTO>> GetRelevantJobsAsync()
        {
            var dbJobs = await _genericJobRepository.GetRangeByPredicateReadOnlyAsync(x => x.JobStatus == JobStatuses.InProgress);

            var result = _mapper.Map<IEnumerable<JobDTO>>(dbJobs);

            return result;
        }

        public async Task<IEnumerable<JobFullInfoDTO>> GetRelevantJobsWithFullInfoAsync()
        {
            var dbJobs = await _genericJobRepository.GetRangeByPredicateReadOnlyAsync(x => x.JobStatus == JobStatuses.InProgress);

            var result = _mapper.Map<IEnumerable<JobFullInfoDTO>>(dbJobs);

            return result;
        }

        public async Task<IEnumerable<JobDTO>> GetStudentJobsAsync(string firstName, string lastName)
        {
            var dbJobs = await _jobRepository.GetStudentJobsAsync(firstName, lastName);

            var result = _mapper.Map<IEnumerable<JobDTO>>(dbJobs);

            return result;
        }
    }
}
