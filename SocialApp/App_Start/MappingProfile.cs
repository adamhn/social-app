using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SocialApp.Dtos;
using SocialApp.Models;

namespace SocialApp.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            //Mapper.CreateMap<UserDto, ApplicationUser>();
        }
    }
}