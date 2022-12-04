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

        public async Task<Guid> AddStudent(StudentCreatingDTO student)
        {
            if(student == null)
            {
                throw new Exception("You are trying to create an empty object");
            }

            if(!student.Role.ToUpper().Equals(Roles.Admin.ToUpper()))
            {
                student.Role = Roles.Student;
            }
            else
            {
                student.Role = Roles.Admin;
            }

            var hashedPassword = _hashService.HashString(student.Password);

            student.Password = hashedPassword;

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
