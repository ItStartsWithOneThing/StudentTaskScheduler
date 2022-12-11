using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentTaskScheduler.BL.Services.JobsService;
using StudentTaskScheduler.BL.Services.StudentsService;
using System;
using System.Threading.Tasks;

namespace StudentTaskScheduler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;  
        private readonly IJobService _jobService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(
            IStudentService studentService,
            IJobService jobService,
            ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _jobService = jobService;
            _logger = logger;
        }


        [HttpGet("GetMyJobs")]
        public async Task<IActionResult> GetMyJobsAsync([FromQuery]string firstName, string lastName)
        {
            var result = await _jobService.GetStudentJobsAsync(firstName, lastName);

            if(result == null)
            {
                return Ok("You don't have any job yet");
            }

            return Ok(result);
        }

        [HttpGet("GetRelevantJobs")]
        public async Task<IActionResult> GetRelevantJobsAsync()
        {
            var result = await _jobService.GetRelevantJobsAsync();

            if (result == null)
            {
                return NotFound("There is no relevant jobs at this moment");
            }

            return Ok(result);
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var result = await _studentService.GetAllStudentsAsync();

            if (result == null)
            {
                _logger.LogWarning("List of students is empty");

                return NotFound("Can't find any student");
            }

            return Ok(result);
        }
    }
}
