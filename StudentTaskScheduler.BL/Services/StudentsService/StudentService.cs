using AutoMapper;
using StudentTaskScheduler.BL.DTOs;
using StudentTaskScheduler.BL.HashService;
using StudentTaskScheduler.DAL.Entities;
using StudentTaskScheduler.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.StudentsService
{
    public class StudentService : IStudentService
    {
        private readonly IDbGenericRepository<Student> _genericStudentRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;

        public StudentService(
            IDbGenericRepository<Student> genericStudentRepository,
            IMapper mapper,
            IHashService hashService)
        {
            _genericStudentRepository = genericStudentRepository;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<Guid> AddStudentAsync(StudentCreatingDTO student)
        {
            if(student == null)
            {
                throw new Exception("You are trying to create an empty object");
            }

            if(student.Role.ToUpper().Equals(Roles.Admin.ToUpper()))
            {
                student.Role = Roles.Admin;
            }
            else
            {
                student.Role = Roles.Student;
            }

            var hashedPassword = _hashService.HashString(student.Password);

            student.Password = hashedPassword;

            var dbStudent = _mapper.Map<Student>(student);

            return await _genericStudentRepository.CreateAsync(dbStudent);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await _genericStudentRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<StudentFullInfoDTO>> GetAllStudentsWithFullInfoAsync()
        {
            var dbStudents = await _genericStudentRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<StudentFullInfoDTO>>(dbStudents);
            
            return result;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var dbStudents = await _genericStudentRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<StudentDTO>>(dbStudents);

            if (result == null || !result.Any())
            {
                throw new Exception("Can't find any student");
            }

            return result;
        }

        public async Task<StudentFullInfoDTO> GetStudentByIdAsync(Guid id)
        {
            var dbStudent = await _genericStudentRepository.GetByIdAsync(id);

            return _mapper.Map<StudentFullInfoDTO>(dbStudent);
        }
    }
}
