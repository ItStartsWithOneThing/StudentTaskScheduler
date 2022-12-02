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
    public class JobProfileProfile : Profile
    {
        public JobProfileProfile()
        {
            CreateMap<JobFullInfoDTO, Job>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ForMember(dest => dest.AssignedToId, opt => opt.MapFrom(src => src.AssignedToId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Job, JobFullInfoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ForMember(dest => dest.AssignedToId, opt => opt.MapFrom(src => src.AssignedToId))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo.FirstName + src.AssignedTo.LastName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));


            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Definition, opt => opt.MapFrom(src => src.Definition))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo.FirstName + src.AssignedTo.LastName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
