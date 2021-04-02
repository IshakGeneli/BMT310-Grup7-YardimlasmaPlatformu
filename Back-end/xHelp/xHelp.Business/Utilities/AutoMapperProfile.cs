using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xHelp.Entity.Concrete;
using xHelp.Entity.DTOs;

namespace xHelp.Business.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateMissionDTO, Mission>().ReverseMap();
            CreateMap<CreateEvidenceDTO, Evidence>().ReverseMap();
            CreateMap<CreateAchievementDTO, Achievement>().ReverseMap();

            CreateMap<UpdateAchievementDTO, Achievement>().ReverseMap();
            CreateMap<UpdateEvidenceDTO, Evidence>().ReverseMap();
            CreateMap<UpdateMissionWithEvidencesDTO, Mission>()
                .ForMember(dest => dest.Evidences, opt =>
                {
                    opt.MapFrom(src => src.UpdateEvidenceDTOs);
                })
                .ReverseMap();
            CreateMap<UpdateMissionDTO, Mission>().ReverseMap();
        }
    }
}
