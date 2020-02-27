using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLibrary.RedisCache
{
    public static class StartupExtension
    {
        public static IServiceCollection AddRedisCache(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new RedisCacheConfigModel();

            configuration
                .GetSection("RedisCache")
                .Bind(config);

            if (!config.IsConfigured())
                throw new InvalidProgramException(
                    "Redis cache not configured.");

            services
                .AddStackExchangeRedisCache(
                    options =>
                    {
                        options.Configuration = config
                            .Configuration;

                        options.InstanceName = config
                            .InstanceName;
                    });

            return services;
        }
    }
}
