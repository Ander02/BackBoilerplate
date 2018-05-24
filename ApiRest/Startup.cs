using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register Services
            BusinessInjector.RegisterServices(Configuration, services);

            DataInjector.RegisterServices(Configuration, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Cors Config
            app.UseCors(builder => builder.AllowAnyOrigin().WithMethods(new string[] { "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS" }).AllowAnyHeader());
            #endregion

            #region Middlewares Config

            #endregion

            app.UseMvc();
        }
    }
}
