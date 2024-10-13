using Ecommerce.Facctories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Extensions
{
    public static class PresentationServicesExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            services.Configure<ApiBehaviorOptions>(options =>                                                 // i changed the default behavior of error response and make it return the custom error response i made
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}
