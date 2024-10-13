using Domain.Contracts;
using Ecommerce.Facctories;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;
using Persistence.Repositories;
using Persistence;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ecommerce.Extensions
{
    public static class InfraStructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSQLConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.
                Connect(configuration.GetConnectionString("Redis")!));
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}
