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
        public IActionResult GetRelevantJobs()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            return Ok();
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
        public IActionResult CreateJob(JobDTO job)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult EndJob(Guid id)
        {
            return Ok();
        }
    }
}
