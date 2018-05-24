using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
