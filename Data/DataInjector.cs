using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public class DataInjector
    {
        private const string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=TodoList;Integrated Security=True";

        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            //DbContext
            services.AddDbContext<Db>((options) => { options.UseSqlServer(_connectionString); });
        }
    }
}
