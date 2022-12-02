using Microsoft.AspNetCore.Mvc;
using System;

namespace StudentTaskScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMyJobs(string firstName, string lastName)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetRelevantJobs()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok();
        }
    }
}
