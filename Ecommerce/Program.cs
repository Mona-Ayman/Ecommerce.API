
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services.Abstractions;
using Services;
using Ecommerce.Middlewares;
using Ecommerce.Facctories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });
            builder.Services.Configure<ApiBehaviorOptions>(options =>                                                 // i changed the default behavior of error response and make it return the custom error response i made
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<GloballErrorHandlingMiddleware>();
            await initializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            //Create object from type that implement IDbInitializer
            async Task initializeDbAsync(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var dbinitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); //IDbInitializer => the interface that has the method of seeding
                await dbinitializer.InitializeAsync();                                 //InitializeAsync => the method inside the interface
            }
        }
    }
}
