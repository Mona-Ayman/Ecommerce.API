
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System.Net;
using System.Text;

namespace Presentation
{
    internal class RedisCacheAttribute(int durationInSec = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            string cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetCachedValueAsync(cacheKey);

            if (result is not null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK,
                };
                return;
            }

            var resultContext = await next.Invoke();
            if (resultContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetCachedValueAsync(cacheKey, resultContext, TimeSpan.FromSeconds(durationInSec));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q => q.Key))
                keyBuilder.Append($"/{item.Key}-{item.Value}");
            return keyBuilder.ToString();
        }
    }
}
