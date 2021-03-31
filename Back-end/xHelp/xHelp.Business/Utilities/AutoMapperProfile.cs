using AutoMapper;
using System;
using System.Collections.Generic;
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
        }
    }
}
