using AutoMapper;
using Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Features.Results.Mappers
{
    class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Task, TaskResult.Full>().ForAllMembers((config) =>
            {
                config.AllowNull();
            });
            ;

            CreateMap<Task, TaskResult.Simple>().ForAllMembers(config =>
            {
                config.AllowNull();
            });
        }
    }
}
