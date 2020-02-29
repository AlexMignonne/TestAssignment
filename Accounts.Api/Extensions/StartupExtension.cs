using System.Collections.Generic;
using Accounts.Api.IntegrationEventHandlers.Addresses.Country;
using Accounts.Api.IntegrationEventHandlers.Addresses.Province;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Api.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                //.AddTransient<AddedCountryIntegrationEventHandler>()
                .AddTransient<RemovedCountryIntegrationEventHandler>()
                //.AddTransient<UpdatedTitleCountryIntegrationEventHandler>()
                //.AddTransient<AddedProvinceIntegrationEventHandler>()
                .AddTransient<RemovedProvinceIntegrationEventHandler>();
            //.AddTransient<UpdatedTitleProvinceIntegrationEventHandler>();

            var rabbitEndpointConfiguration = services
                .AddRabbitMq(
                    configuration,
                    new List<RabbitExchange>
                    {
                        //new AddedCountryExchange(),
                        new RemovedCountryExchange(),
                        //new UpdatedTitleCountryExchange(),
                        //new AddedProvinceExchange(),
                        new RemovedProvinceExchange()
                        //new UpdatedTitleProvinceExchange()
                    });

            var rabbitHandlerManager = new RabbitHandlerManager(
                services
                    .BuildServiceProvider());

            //rabbitHandlerManager
            //    .Add<
            //        AddedCountryIntegrationEventHandler,
            //        AddedCountryIntegrationEvent,
            //        AddedCountryExchange>(
            //        rabbitEndpointConfiguration,
            //        "country_added.account_service");

            rabbitHandlerManager
                .Add<
                    RemovedCountryIntegrationEventHandler,
                    RemovedCountryIntegrationEvent,
                    RemovedCountryExchange>(
                    rabbitEndpointConfiguration,
                    "country_removed.account_service");

            //rabbitHandlerManager
            //    .Add<
            //        UpdatedTitleCountryIntegrationEventHandler,
            //        UpdatedTitleCountryIntegrationEvent,
            //        UpdatedTitleCountryExchange>(
            //        rabbitEndpointConfiguration,
            //        "country_updated_title.account_service");

            //rabbitHandlerManager
            //    .Add<
            //        AddedProvinceIntegrationEventHandler,
            //        AddedProvinceIntegrationEvent,
            //        AddedProvinceExchange>(
            //        rabbitEndpointConfiguration,
            //        "province_added.account_service");

            rabbitHandlerManager
                .Add<
                    RemovedProvinceIntegrationEventHandler,
                    RemovedProvinceIntegrationEvent,
                    RemovedProvinceExchange>(
                    rabbitEndpointConfiguration,
                    "province_removed.account_service");

            //rabbitHandlerManager
            //    .Add<
            //        UpdatedTitleProvinceIntegrationEventHandler,
            //        UpdatedTitleProvinceIntegrationEvent,
            //        UpdatedTitleProvinceExchange>(
            //        rabbitEndpointConfiguration,
            //        "province_updated_title.account_service");

            return services;
        }
    }
}
