using AutoMapper;
using StudentTaskScheduler.BL.DTOs;
using StudentTaskScheduler.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Automapper.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentFullInfoDTO, Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(dest => dest.Faculty, opt => opt.MapFrom(src => src.Faculty))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<Student, StudentFullInfoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(dest => dest.Faculty, opt => opt.MapFrom(src => src.Faculty))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs));

            CreateMap<Student, StudentDTO>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
               .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
               .ForMember(dest => dest.Faculty, opt => opt.MapFrom(src => src.Faculty));
        }
    }
}
