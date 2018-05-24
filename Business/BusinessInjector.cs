using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class BusinessInjector
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddMvc((options) =>
            {
                //options.Filters.Add(typeof(ValidationActionFilter));
            })
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<BusinessInjector>())
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            services.AddMediatR(typeof(BusinessInjector));
        }
    }
}
