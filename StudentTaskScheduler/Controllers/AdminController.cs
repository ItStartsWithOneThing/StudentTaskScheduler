using Microsoft.AspNetCore.Mvc;
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

        public AdminController(
            IJobService jobService,
            IStudentService studentService,
            IAuthorizationService authService)
        {
            _jobService = jobService;
            _studentService = studentService;
            _authService = authService;
        }

        [HttpGet("SignIn/{login}/{password}")]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            string token;

            try
            {
                token = await _authService.SignIn(login, password);
            }

            catch (ArgumentException)
            {
                return Unauthorized();
            }

            return token != null ? Ok(token) : Unauthorized();
        }

        //[HttpPost]
        //public async Task<IActionResult> SignUp(StudentFullInfoDTO user)
        //{
        //    var result = await _authService.SignUp(user);

        //    return Ok();
        //}

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

            return Created(result.ToString(), job);
        }

        [HttpPatch("EndJob")]
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
