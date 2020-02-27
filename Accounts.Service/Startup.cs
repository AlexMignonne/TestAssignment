using System.Collections.Generic;
using Accounts.Api;
using Accounts.Application.Extensions;
using Accounts.Infrastructure.Extensions;
using Accounts.Service.IntegrationEventHandlers.Addresses.Country;
using Accounts.Service.IntegrationEventHandlers.Addresses.Province;
using Accounts.Service.Middlewares;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;
using CommonLibrary.RedisCache;
using CommonLibrary.RequestInfo;
using CommonLibrary.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Service
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            services
                .AddMediatR(
                    GetType()
                        .Assembly,
                    typeof(Application.Extensions.StartupExtension)
                        .Assembly,
                    typeof(Infrastructure.Extensions.StartupExtension)
                        .Assembly
                )
                .AddRedisCache(
                    Configuration)
                .AddApplicationServices()
                .AddInfrastructureServices(
                    Configuration)
                .AddSwagger(
                    Configuration)
                .AddControllers()
                .AddApplicationPart(
                    typeof(IApiAssembly).Assembly);

            var rabbitEndpointConfiguration = services
                .AddRabbitMq(
                    Configuration,
                    new List<RabbitExchange>
                    {
                        new AddedCountryExchange(),
                        new RemovedCountryExchange(),
                        new UpdatedTitleCountryExchange(),
                        new AddedProvinceExchange(),
                        new RemovedProvinceExchange(),
                        new UpdatedTitleProvinceExchange()
                    });

            services
                .AddSingleton(
                    new AddCountryIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new AddedCountryExchange()))
                .AddSingleton(
                    new RemoveCountryIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new RemovedCountryExchange()))
                .AddSingleton(
                    new UpdateTitleCountryIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new UpdatedTitleCountryExchange()))
                .AddSingleton(
                    new AddProvinceIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new AddedProvinceExchange()))
                .AddSingleton(
                    new RemoveProvinceIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new RemovedProvinceExchange()))
                .AddSingleton(
                    new UpdateTitleProvinceIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new UpdatedTitleProvinceExchange()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            app
                //.UseHttpsRedirection()
                .UseRouting()
                .UseMiddleware<ExceptionMiddleware>()
                .UseRequestInfo()
                .UseAuthorization()
                .UseSwagger(
                    Configuration)
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app
                .ApplicationServices
                .ApplyMigration();
        }
    }
}
