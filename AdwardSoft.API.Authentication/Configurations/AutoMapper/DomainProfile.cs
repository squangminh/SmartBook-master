using AdwardSoft.DTO.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Configurations.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        { 
            CreateMap<ApplicationUser, UserInfo>();
        }
    }
}
