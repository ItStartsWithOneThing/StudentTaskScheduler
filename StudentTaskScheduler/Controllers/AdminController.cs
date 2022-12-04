using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentTaskScheduler.BL.DTOs;
using StudentTaskScheduler.BL.Services.AuthorizationService;
using StudentTaskScheduler.BL.Services.JobsService;
using StudentTaskScheduler.BL.Services.StudentsService;
using System;
using System.Threading.Tasks;

namespace StudentTaskScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IStudentService _studentService;
        private readonly IAuthorizationService _authService;   
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            IJobService jobService,
            IStudentService studentService,
            IAuthorizationService authService,
            ILogger<AdminController> logger)
        {
            _jobService = jobService;
            _studentService = studentService;
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("SignIn/{login}/{password}")]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            string token;

            try
            {
                token = await _authService.SignIn(login, password);
            }

            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation($"User {login} authorization has failed wit message: {ex.Message}");

                return Unauthorized();
            }

            _logger.LogInformation($"User {login} successfully authorized");

            return token != null ? Ok(token) : Unauthorized();
        }

        [HttpGet("GetStudent/{id}")]
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

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult> GetAllStudentsWithFullInfo()
        {
            var result = await _studentService.GetAllStudentsWithFullInfo();

            if (result != null)
            {
                return Ok(result);
            }

            _logger.LogInformation("List of students is empty");

            return NotFound(result);
        }

        [HttpGet("GetRelevantJobs")]
        public async Task<IActionResult> GetRelevantJobsWithFullInfo()
        {
            var result = await _jobService.GetRelevantJobsWithFullInfo();

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound("There is no relevant jobs at this moment");
        }

        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobsWithFullInfo()
        {
            var result = await _jobService.GetAllJobsWithFullInfo();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(StudentCreatingDTO student)
        {
            var result = await _studentService.AddStudent(student);

            if (!result.Equals(Guid.Empty))
            {
                student.Id = result;

                _logger.LogInformation($"Created new student id: {student.Id}");

                return Created(result.ToString(), student);   
            }

            return BadRequest();
        }


        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteStudent(id);

            if(result == true)
            {
                return NoContent();
            }

            _logger.LogInformation($"Student with id: {id} has been deleted");

            return NotFound();
        }

        [HttpPost("CreateJob")]
        public async Task<IActionResult> CreateJob(JobFullInfoDTO job)
        {
            var result = await _jobService.CreateJob(job);

            if(result.Equals(Guid.Empty))
            {
                return BadRequest($"Student with id: {job.AssignedToId} doesn't exist");
            }

            _logger.LogInformation($"Added new job with id: {result}");

            return Created(result.ToString(), job);
        }

        [HttpPatch("EndJob")]
        public async Task<IActionResult> EndJob(Guid id)
        {
            var result = await _jobService.EndJob(id);

            if(result)
            {
                _logger.LogInformation($"The job with id: {id} is completed");

                return Ok();
            }

            return NotFound(id);
        }
    }
}
