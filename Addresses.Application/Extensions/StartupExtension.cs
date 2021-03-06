﻿using Addresses.Application.DomainEventHandlers.Country;
using Addresses.Application.DomainEventHandlers.Province;
using Addresses.Application.UseCases.Country;
using Addresses.Application.UseCases.Province;
using Microsoft.Extensions.DependencyInjection;

namespace Addresses.Application.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services
                .AddTransient<AddCountryUseCase>()
                .AddTransient<GetByIdCountryUseCase>()
                .AddTransient<GetByIdProvinceUseCase>()
                .AddTransient<GetListCountryUseCase>()
                .AddTransient<IsExistProvinceByIdUseCase>()
                .AddTransient<RemoveCountryUseCase>()
                .AddTransient<UpdateTitleCountryUseCase>()
                .AddTransient<AddProvinceUseCase>()
                .AddTransient<GetByIdProvinceUseCase>()
                .AddTransient<RemoveProvinceUseCase>()
                .AddTransient<UpdateTitleProvinceUseCase>()
                .AddTransient<AddedCountryDomainEventHandler>()
                .AddTransient<RemovedProvinceDomainEventHandler>()
                .AddTransient<UpdatedTitleCountryDomainEventHandler>()
                .AddTransient<AddedProvinceDomainEventHandler>()
                .AddTransient<RemovedProvinceDomainEventHandler>()
                .AddTransient<UpdatedTitleProvinceDomainEventHandler>();

            return services;
        }
    }
}
