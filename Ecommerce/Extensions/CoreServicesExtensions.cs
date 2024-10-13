using Services.Abstractions;
using Services;

namespace Ecommerce.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            return services;
        }
    }
}
