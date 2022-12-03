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

        public async Task<Guid> CreateJob(JobFullInfoDTO job)
        {
            if (job == null)
            {
                throw new Exception("You are trying to create an empty object");
            }

            var dbStudent = await _genericStudentRepository.GetById(job.AssignedToId);

            if(dbStudent == null)
            {
                return Guid.Empty;
            }

            job.AssignedTo = dbStudent.FirstName + " " + dbStudent.LastName;
            job.StartDate = DateTime.Now;
            job.JobStatus = JobStatuses.InProgress;

            var dbJob = _mapper.Map<Job>(job);

            return await _genericJobRepository.Create(dbJob);
        }

        public async Task<bool> EndJob(Guid id)
        {
            return await _jobRepository.EndJob(id, JobStatuses.Done);
        }

        public async Task<IEnumerable<JobFullInfoDTO>> GetAllJobsWithFullInfo()
        {
            var dbJobs = await _genericJobRepository.GetAll();

            var result = _mapper.Map<IEnumerable<JobFullInfoDTO>>(dbJobs);

            return result;
        }

        public async Task<IEnumerable<JobFullInfoDTO>> GetRelevantJobsWithFullInfo()
        {
            var dbJobs = await _genericJobRepository.GetRangeByPredicateReadOnly(x => x.JobStatus == JobStatuses.InProgress);

            var result = _mapper.Map<IEnumerable<JobFullInfoDTO>>(dbJobs);

            return result;
        }

        public Task<IEnumerable<JobDTO>> GetStudentJobs(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}
