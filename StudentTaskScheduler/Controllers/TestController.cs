using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public TestController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("MakeAdminSeed")]
        public async Task<IActionResult> MakeAdminSeed(StudentCreatingDTO student)
        {
            var result = await _studentService.AddStudent(student);

            if (!result.Equals(Guid.Empty))
            {
                student.Id = result;

                return Created(result.ToString(), student);
            }

            return BadRequest();
        }
    }
}
