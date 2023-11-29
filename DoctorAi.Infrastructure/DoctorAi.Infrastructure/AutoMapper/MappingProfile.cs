using AutoMapper;
using DoctorAi.Domain._01.Entities._02.Verifycode;
using DoctorAi.Domain._01.Entities.Users;
using DoctorAi.Infrastructure._02.UserService;
using DoctorAi.Infrastructure._03.SmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            CreateMap<UserApp, CreateUserDto>().ReverseMap();
            CreateMap<UserApp,UpdateUserDto>().ReverseMap();
            CreateMap<VerifyCode, VerifycodeDto>().ReverseMap();
        }
    }
}
