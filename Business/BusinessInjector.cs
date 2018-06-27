using AutoMapper;
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
                config.AddProfiles(typeof(BusinessInjector));
            });

            services.AddMediatR(typeof(BusinessInjector));
        }
    }
}