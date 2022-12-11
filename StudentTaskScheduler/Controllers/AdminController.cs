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

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody]SignInCredentialsDTO credentials)
        {
            string token;

            try
            {
                token = await _authService.SignInAsync(credentials.Login, credentials.Password);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError($"User {credentials.Login} authorization has failed wit message: {ex.Message}");

                return Unauthorized();
            }

            _logger.LogInformation($"User {credentials.Login} successfully authorized");

            return token != null ? Ok(token) : Unauthorized();
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudentById([FromQuery]Guid id)
        {
            if(id.Equals(Guid.Empty))
            {
                return BadRequest(id);
            }

            var result = await _studentService.GetStudentByIdAsync(id);

            if (result == null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult> GetAllStudentsWithFullInfo()
        {
            var result = await _studentService.GetAllStudentsWithFullInfoAsync();

            if (result == null)
            {
                    _logger.LogWarning("List of students is empty");

                    return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("GetRelevantJobs")]
        public async Task<IActionResult> GetRelevantJobsWithFullInfo()
        {
            var result = await _jobService.GetRelevantJobsWithFullInfoAsync();

            if(result == null)
            {
                return NotFound("There is no relevant jobs at this moment");
            }

            return Ok(result);
        }

        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobsWithFullInfo()
        {
            var result = await _jobService.GetAllJobsWithFullInfoAsync();

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromForm]StudentCreatingDTO student)
        {
            var result = await _studentService.AddStudentAsync(student);

            if (result.Equals(Guid.Empty))
            {
                return BadRequest();
            }

            student.Id = result;

            _logger.LogInformation($"Created new student id: {student.Id}");

            return Created(result.ToString(), student);
        }


        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromQuery]Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            if(result)
            {
                _logger.LogInformation($"Student with id: {id} has been deleted");
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("CreateJob")]
        public async Task<IActionResult> CreateJob([FromForm]JobFullInfoDTO job)
        {
            var result = await _jobService.CreateJobAsync(job);

            if(result.Equals(Guid.Empty))
            {
                return BadRequest($"Student with id: {job.AssignedToId} doesn't exist");
            }

            _logger.LogInformation($"Added new job with id: {result}");

            return Created(result.ToString(), job);
        }

        [HttpPatch("EndJob")]
        public async Task<IActionResult> EndJob([FromQuery]Guid id)
        {
            var result = await _jobService.EndJobAsync(id);

            if(result)
            {
                _logger.LogInformation($"The job with id: {id} is completed");

                return Ok();
            }

            return NotFound(id);
        }
    }
}
