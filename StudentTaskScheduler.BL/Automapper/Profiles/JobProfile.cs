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
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobFullInfoDTO, Job>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.AssignedToId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.JobStatus));

            CreateMap<Job, JobFullInfoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ForMember(dest => dest.AssignedToId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.Student.FirstName + src.Student.LastName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.JobStatus));


            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.Student.FirstName + src.Student.LastName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.JobStatus));
        }
    }
}
