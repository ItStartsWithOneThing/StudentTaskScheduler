﻿using StudentTaskScheduler.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.StudentsService
{
    public interface IStudentService
    {
        Task<IEnumerable<JobDTO>> GetStudentJobs(string firstName, string lastName);
        Task<IEnumerable<StudentDTO>> GetAllStudents();
        Task<StudentFullInfoDTO> GetStudentById(Guid id);
        Task<Guid> AddStudent(StudentFullInfoDTO student);
        Task<IEnumerable<StudentFullInfoDTO>> GetAllStudentsWithFullInfo();
        Task<bool> DeleteStudent(Guid id);
    }
}
