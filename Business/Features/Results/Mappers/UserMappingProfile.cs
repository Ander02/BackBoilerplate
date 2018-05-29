using AutoMapper;
using Data.Domain;

namespace Business.Features.Results.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResult.Full>();

            CreateMap<User, UserResult.Simple>();

            CreateMap<Users.Register.Command, User>();
        }
    }
}
