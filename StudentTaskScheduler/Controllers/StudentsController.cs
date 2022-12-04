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


        [HttpGet("GetMyJobs/{firstName}/{lastName}")]
        public async Task<IActionResult> GetMyJobs(string firstName, string lastName)
        {
            var result = await _jobService.GetStudentJobs(firstName, lastName);

            if(result != null)
            {
                return Ok(result);
            }

            return Ok("You don't have any job yet");
        }

        [HttpGet("GetRelevantJobs")]
        public async Task<IActionResult> GetRelevantJobs()
        {
            var result = await _jobService.GetRelevantJobs();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("There is no relevant jobs at this moment");
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllStudents();

            if (result != null)
            {
                return Ok(result);
            }

            _logger.LogInformation("List of students is empty");

            return NotFound("Can't find any student");
        }
    }
}
