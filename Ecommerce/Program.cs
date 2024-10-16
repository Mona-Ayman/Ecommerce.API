
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
using Ecommerce.Extensions;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services
            // Add services to the container.
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            #endregion

            #region Pipelines
            var app = builder.Build();
            app.UseCustomExceptionMiddleware();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion



        }
    }
}
