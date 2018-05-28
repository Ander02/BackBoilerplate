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
            CreateMap<User, UserResult.Full>();

            CreateMap<User, UserResult.Simple>();

            CreateMap<Users.Register.Command, User>();            
        }
    }
}
