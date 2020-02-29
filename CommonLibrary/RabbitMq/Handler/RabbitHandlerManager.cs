using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CommonLibrary.RabbitMq.Handler
{
    public sealed class RabbitHandlerManager
    {
        private static readonly ConcurrentDictionary<Type, object> Handlers
            = new ConcurrentDictionary<Type, object>();

        private readonly IServiceProvider _serviceProvider;

        public RabbitHandlerManager(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool Add<TRabbitHandler, TMessage, TExchange>(
            RabbitEndpointConfiguration endpointConfiguration,
            string queue,
            bool durable = true,
            bool exclusive = false,
            bool autoDelete = false,
            bool autoAct = false,
            IReadOnlyCollection<string>? routingKeys = null,
            IDictionary<string, object>? arguments = null)
            where TRabbitHandler : IRabbitHandler<TMessage, TExchange>
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new()
        {
            var exchange = new TExchange();

            var handler = new RabbitHandler<TRabbitHandler, TMessage, TExchange>(
                _serviceProvider,
                endpointConfiguration,
                exchange,
                queue,
                durable,
                exclusive,
                autoDelete,
                autoAct,
                routingKeys,
                arguments);

            var type = typeof(
                IRabbitHandler<TMessage, TExchange>);

            return Handlers
                .TryAdd(
                    type,
                    handler);
        }

        public bool Remove<TMessage, TExchange>()
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new()
        {
            var type = typeof(
                IRabbitHandler<TMessage, TExchange>);

            return Handlers
                .TryRemove(
                    type,
                    out _);
        }
    }
}
