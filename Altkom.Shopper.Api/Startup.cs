using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            // Under construction
            // app.Run(context => context.Response.WriteAsync("Under construction"));

            // Logger
            app.Use(async (context, next) =>
            {
                logger.LogInformation($"{context.Request.Method} {context.Request.Path}");

                await next();

                logger.LogInformation($"{context.Response.StatusCode}");
            });

            // Authorization
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    await next();
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            });

            // Logic
            app.Run(context => context.Response.WriteAsync("Hello World!"));
        }
    }
}
