using Altkom.Shopper.Api.Middlewares;
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

            // Under construction Middleware
            // app.Run(context => context.Response.WriteAsync("Under construction"));

            // Logger Middleware
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation($"{context.Request.Method} {context.Request.Path}");

            //    await next();

            //    logger.LogInformation($"{context.Response.StatusCode}");
            //});


            // Authorization Middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    }
            //});


            if (env.IsDevelopment())
            {
                app.UseLogger();
            }

            app.UseMiddleware<FormatMiddleware>();

            // app.UseMiddleware<AuthorizationMiddleware>();

           // app.UseMyAuthorization();

            // Customers Middleware
            app.Use(async (context, next) =>
            {
                if (context.Request.Path=="api/customers")
                {

                }
                else
                {
                    await next();
                }
            });

            // Logic
            app.Run(context => context.Response.WriteAsync("Hello World!"));
        }
    }
}
