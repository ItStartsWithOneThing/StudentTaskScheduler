using Microsoft.AspNetCore.Mvc;
using StudentTaskScheduler.BL.Services.JobsService;
using StudentTaskScheduler.BL.Services.StudentsService;
using System;
using System.Threading.Tasks;

namespace StudentTaskScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;  
        private readonly IJobService _jobService;

        public StudentsController(
            IStudentService studentService,
            IJobService jobService)
        {
            _studentService = studentService;
            _jobService = jobService;
        }


        [HttpGet]
        public async Task<IActionResult> GetMyJobs(string firstName, string lastName)
        {
            var result = await _jobService.GetStudentJobs(firstName, lastName);

            if(result != null)
            {
                return Ok(result);
            }

            return Ok("You don't have any job yet");
        }

        [HttpGet]
        public async Task<IActionResult> GetRelevantJobs()
        {
            var result = await _jobService.GetRelevantJobs();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("There is no relevant jobs at this moment");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllStudents();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("Can't find any student");
        }
    }
}
