using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Sandogh.Admin.EndPoint.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class InitDataBaseMiddleware
    {
        private readonly RequestDelegate _next;

        public InitDataBaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class InitDataBaseMiddlewareExtensions
    {
        public static IApplicationBuilder UseInitDataBaseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InitDataBaseMiddleware>();
        }
    }
}
