using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentTaskScheduler.BL.DTOs;
using StudentTaskScheduler.BL.Services.StudentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTaskScheduler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<TestController> _logger;

        public TestController(
            IStudentService studentService,
            ILogger<TestController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpPost("MakeAdminSeed")]
        public async Task<IActionResult> MakeAdminSeed(StudentCreatingDTO student)
        {
            var result = await _studentService.AddStudent(student);

            if (!result.Equals(Guid.Empty))
            {
                student.Id = result;

                _logger.LogInformation($"New seed with id: {student.Id} has been created");

                return Created(result.ToString(), student);
            }

            _logger.LogInformation("Failed attempt to create new seed");

            return BadRequest();
        }
    }
}
