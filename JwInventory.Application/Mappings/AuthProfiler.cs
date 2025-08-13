using AutoMapper;
using JwInventory.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.Mappings
{
    public class AuthProfiler : Profile
    {
        public AuthProfiler()
        {
            CreateMap<RegisterUserDto, UserResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Email));
        }
    }
}
