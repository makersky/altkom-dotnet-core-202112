using Altkom.Shopper.FakeRepositories;
using Altkom.Shopper.Fakers;
using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Altkom.Shopper.Models.Validators;
using Altkom.Shopper.WebApi.HealthChecks;
using Altkom.Shopper.WebApi.Hubs;
using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<ICustomerRepository, FakeCustomerRepository>();

            services.AddSingleton<Faker<Product>, ProductFaker>();
            services.AddSingleton<IProductRepository, FakeProductRepository>();

            services.AddTransient<IValidator<Product>, ProductValidator>();

            // Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation() // FluentValidation.AspNetCore
                ;
               
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Altkom.Shopper.WebApi", Version = "v1" });
            });


            services.AddSignalR();

            services.AddHealthChecks()
                .AddCheck("Ping", () => HealthCheckResult.Healthy())
                .AddCheck<RandomHealthCheck>("random");

            // https://github.com/xabaril/AspNetCore.Diagnostics.HealthChecks

            // Install-Package AspNetCore.HealthCheck.UI            
            services.AddHealthChecksUI()
                .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Altkom.Shopper.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();                            

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("api/orders", async context =>
                {
                    await context.Response.WriteAsync("Hello Orders!");
                });

                endpoints.MapGet("api/orders/{id:int}", async context =>
                {
                    int id = Convert.ToInt32(context.Request.RouteValues["id"]);

                    await context.Response.WriteAsync("Hello Orders!");
                });

                // .NET 6 (Minimal API)
                // endpoints.MapGet("api/orders/{id:int}", (int id, IOrderRepository orderRepository) => orderRepository.Get(id))

                endpoints.MapControllers();

                endpoints.MapHub<CustomersHub>("/signalr/customers");

                // Install-Package AspNetCore.HealthCheck.UI.Client
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                // http://localhost:5000/healthchecks-ui
                endpoints.MapHealthChecksUI();
            });
        }
    }
}
