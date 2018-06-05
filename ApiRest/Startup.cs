using ApiRest.Infraestructure.Filters;
using ApiRest.Infraestructure.Middlewares;
using Business;
using Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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
            services.AddMvc((options) =>
            {
                options.Filters.Add(typeof(ValidationActionFilter));
            })
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<BusinessInjector>())
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            //Register Services
            BusinessInjector.RegisterServices(Configuration, services);

            DataInjector.RegisterServices(Configuration, services);

            //Authentication
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;

                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidateIssuerSigningKey = true,

                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Token:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["Token:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization((options) =>
            {
                options.AddPolicy("IsAdmin", (policy) =>
                {
                    policy.RequireClaim("Role", "Admin");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }

            #region Cors Config
            app.UseCors(builder => builder.AllowAnyOrigin().WithMethods(new string[] { "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS" }).AllowAnyHeader());
            #endregion

            #region Middlewares Config
            app.UseMiddleware<HttpExceptionHandlerMiddleware>();
            #endregion

            app.UseMvc();
        }
    }
}
