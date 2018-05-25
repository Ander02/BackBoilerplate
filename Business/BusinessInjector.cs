using AutoMapper;
using Business.Features.Results.Mappers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public class BusinessInjector
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddAutoMapper((config) =>
            {
                //config.AddProfile<UserMappingProfile>();
                //config.AddProfile<TaskMappingProfile>();
                config.AddProfiles(typeof(BusinessInjector));
            });

            services.AddMediatR(typeof(BusinessInjector));
        }
    }
}
