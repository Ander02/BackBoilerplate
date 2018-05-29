using AutoMapper;
using Data.Domain;

namespace Business.Features.Results.Mappers
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Task, TaskResult.Full>();

            CreateMap<Task, TaskResult.Simple>();

            CreateMap<Tasks.Register.Command, Task>();
        }
    }
}
