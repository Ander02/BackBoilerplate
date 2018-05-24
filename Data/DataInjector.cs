using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataInjector
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            //DbContext
            services.AddDbContext<Db>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
