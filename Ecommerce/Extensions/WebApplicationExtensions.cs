using Domain.Contracts;
using Ecommerce.Middlewares;

namespace Ecommerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            //Create object from type that implement IDbInitializer
            using var scope = app.Services.CreateScope();
            var dbinitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); //IDbInitializer => the interface that has the method of seeding
            await dbinitializer.InitializeAsync();                                 //InitializeAsync => the method inside the interface
        
            return app;
        }

        public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GloballErrorHandlingMiddleware>();
            return app;
        }
    }
}
