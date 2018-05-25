using AutoMapper;
using Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Features.Results.Mappers
{
    class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResult.Full>().ForAllMembers(config => { });

            CreateMap<User, UserResult.Simple>().ForAllMembers(config => { });

            CreateMap<User, Users.Register.Command>().ForAllMembers(config => { });
        }
    }
}
