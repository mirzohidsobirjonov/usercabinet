using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCabinet.Api.Extensions;
using UserCabinet.Api.Middlewares;
using UserCabinet.Data.DbContexts;
using UserCabinet.Service.Helpers;
using UserCabinet.Service.Mappers;

namespace UserCabinet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserCabinetDbContext>(options 
                => options.UseNpgsql(Configuration.GetConnectionString("UserCabinetDb")));

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserCabinet.Api", Version = "v1" });
            });

            services.AddCustomServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserCabinet.Api v1"));
            }

            EnvironmentHelper.WebRootPath = app.ApplicationServices.GetService<IWebHostEnvironment>().WebRootPath;

            app.UseMiddleware<UserExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
