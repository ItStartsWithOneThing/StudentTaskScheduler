using Microsoft.AspNetCore.Mvc;
using StudentTaskScheduler.BL.DTOs;
using StudentTaskScheduler.BL.Services.JobsService;
using StudentTaskScheduler.BL.Services.StudentsService;
using System;
using System.Threading.Tasks;

namespace StudentTaskScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IStudentService _studentService;

        [HttpGet]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            if(!id.Equals(Guid.Empty))
            {
                var result = await _studentService.GetStudentById(id);

                if(result !=null)
                {
                    return Ok(result);
                }

                return NotFound(id);
            }

            return BadRequest(id);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudentsWithFullInfo()
        {
            var result = await _studentService.GetAllStudentsWithFullInfo();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRelevantJobsWithFullInfo()
        {
            var result = await _jobService.GetRelevantJobsWithFullInfo();

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound("There is no relevant jobs at this moment");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobsWithFullInfo()
        {
            var result = await _jobService.GetAllJobsWithFullInfo();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentFullInfoDTO student)
        {
            var result = await _studentService.AddStudent(student);

            if (!result.Equals(Guid.Empty))
            {
                student.Id = result;

                return Created(result.ToString(), student);   
            }

            return BadRequest();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteStudent(id);

            if(result == true)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(JobFullInfoDTO job)
        {
            var result = await _jobService.CreateJob(job);

            if(result.Equals(Guid.Empty))
            {
                return BadRequest($"Student with id: {job.AssignedToId} doesn't exist");
            }

            return Created(result.ToString(), job);
        }

        [HttpPatch]
        public async Task<IActionResult> EndJob(Guid id)
        {
            var result = await _jobService.EndJob(id);

            if(result)
            {
                return Ok();
            }

            return NotFound(id);
        }
    }
}
