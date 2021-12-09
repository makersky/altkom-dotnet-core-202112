using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.Api.Middlewares
{
    
    public static class LoggerMiddlewareExtensions
    {
        // Metoda rozszerzająca (Extension Method)
        public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggerMiddleware>();

            return app;
        }
    }

    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggerMiddleware> logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)  // konstruktor musi posiadać parametr RequestDelegate
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation($"{context.Request.Method} {context.Request.Path}");

            await next(context);

            logger.LogInformation($"{context.Response.StatusCode}");
        }
    }
}
