using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Altkom.Shopper.Api.Middlewares
{
    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyAuthorization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationMiddleware>();
        }
    }

    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }

    public class FormatMiddleware
    {
        private readonly RequestDelegate next;

        public FormatMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Query.TryGetValue("format", out var value))
            {
                context.Request.Headers.Add("Content-Type", value);                               
            }

            await next(context);
        }

    }
}
