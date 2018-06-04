using Data.Database;
using Data.Domain;
using Data.Domain.Identitty;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public class DataInjector
    {
        //Move to settings.json after
        private const string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TodoList;Integrated Security=True";

        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            //DbContext
            services.AddDbContext<Db>((options) => { options.UseSqlServer(_connectionString); });

            services.AddIdentity<User, Role>((options) =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                
            }).AddEntityFrameworkStores<Db>(); ;
        }
    }
}
