using StudentTaskScheduler.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.StudentsService
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentFullInfoDTO> GetStudentByIdAsync(Guid id);
        Task<Guid> AddStudentAsync(StudentCreatingDTO student);
        Task<IEnumerable<StudentFullInfoDTO>> GetAllStudentsWithFullInfoAsync();
        Task<bool> DeleteStudentAsync(Guid id);
    }
}
