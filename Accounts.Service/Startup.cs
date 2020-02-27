using System.Collections.Generic;
using Accounts.Api;
using Accounts.Api.IntegrationEventHandlers.Addresses.Country;
using Accounts.Api.IntegrationEventHandlers.Addresses.Province;
using Accounts.Application.UseCases.Extensions;
using Accounts.Infrastructure.Extensions;
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
using StartupExtension = Accounts.Application.UseCases.Extensions.StartupExtension;

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
                    typeof(StartupExtension)
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
                    new AddedCountryIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new AddedCountryExchange()))
                .AddSingleton(
                    new RemovedCountryIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new RemovedCountryExchange(),
                        services
                            .BuildServiceProvider()))
                .AddSingleton(
                    new UpdatedTitleCountryIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new UpdatedTitleCountryExchange()))
                .AddSingleton(
                    new AddedProvinceIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new AddedProvinceExchange()))
                .AddSingleton(
                    new RemovedProvinceIntegrationEventHandler(
                        rabbitEndpointConfiguration,
                        new RemovedProvinceExchange()))
                .AddSingleton(
                    new UpdatedTitleProvinceIntegrationEventHandler(
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
