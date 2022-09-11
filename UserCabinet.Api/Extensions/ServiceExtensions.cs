using Microsoft.Extensions.DependencyInjection;
using UserCabinet.Data.IRepositories;
using UserCabinet.Data.Repositories;
using UserCabinet.Service.Interfaces;
using UserCabinet.Service.Services;

namespace UserCabinet.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAttechmentService, AttechmentService>();
        }
    }
}
