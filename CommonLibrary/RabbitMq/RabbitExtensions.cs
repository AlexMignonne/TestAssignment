using System;
using System.Collections.Generic;
using CommonLibrary.RabbitMq.Declare;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLibrary.RabbitMq
{
    public static class RabbitExtensions
    {
        public static RabbitEndpointConfiguration AddRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration,
            List<RabbitExchange> exchanges)
        {
            var config = new RabbitConfig();

            configuration
                .GetSection("RabbitMQ")
                .Bind(config);

            if (!config.IsConfigured())
                throw new InvalidProgramException(
                    "RabbitMQ not configured.");

            foreach (var exchange in exchanges)
                RabbitExchangeDeclare
                    .Add(exchange);

            var endpointConfiguration = new RabbitEndpointConfiguration(
                config.Uri);

            services
                .AddSingleton(endpointConfiguration)
                .AddTransient<IRabbitPublisher, RabbitPublisher>();

            return endpointConfiguration;
        }
    }
}
