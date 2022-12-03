using AutoMapper;
using StudentTaskScheduler.BL.DTOs;
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

        public StudentService(
            IDbGenericRepository<Student> genericStudentRepository,
            IMapper mapper)
        {
            _genericStudentRepository = genericStudentRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddStudent(StudentFullInfoDTO student)
        {
            if(student == null)
            {
                throw new Exception("You are trying to create an empty object");
            }

            if(!student.Role.Equals(Roles.Admin))
            {
                student.Role = Roles.Student;
            }

            var dbStudent = _mapper.Map<Student>(student);

            return await _genericStudentRepository.Create(dbStudent);
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            return await _genericStudentRepository.DeleteById(id);
        }

        public async Task<IEnumerable<StudentFullInfoDTO>> GetAllStudentsWithFullInfo()
        {
            var dbStudents = await _genericStudentRepository.GetAll();

            var result = _mapper.Map<IEnumerable<StudentFullInfoDTO>>(dbStudents);
            
            return result;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
            var dbStudents = await _genericStudentRepository.GetAll();

            var result = _mapper.Map<IEnumerable<StudentDTO>>(dbStudents);

            if (result == null || !result.Any())
            {
                throw new Exception("Can't find any student");
            }

            return result;
        }

        public async Task<StudentFullInfoDTO> GetStudentById(Guid id)
        {
            var dbStudent = await _genericStudentRepository.GetById(id);

            return _mapper.Map<StudentFullInfoDTO>(dbStudent);
        }
    }
}
