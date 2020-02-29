using System;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMq.Publisher
{
    public sealed class RabbitPublisher
        : IRabbitPublisher
    {
        private readonly IModel _model;

        public RabbitPublisher(
            RabbitEndpointConfiguration endpointConfiguration)
        {
            _model = endpointConfiguration.Model;
        }

        public Task Publish<TMessage, TExchange>(
            TMessage message,
            string? routingKey = null,
            bool mandatory = false,
            IBasicProperties? properties = null)
            where TMessage : Message<TExchange>
            where TExchange : RabbitExchange, new()
        {
            var exchange = RabbitExchangeManager
                .Get(
                    message.ExchangeName);

            if (exchange == null)
                throw new ArgumentException(
                    $"Message {message.ExchangeName} not registered in exchange.");

            if (exchange.Type == RabbitExchangeType.Fanout &&
                !string.IsNullOrWhiteSpace(routingKey))
                throw new ArgumentException(
                    "Fanout cannot use routing key.");

            _model.ExchangeDeclare(
                exchange.Name,
                exchange
                    .Type
                    .ToString()
                    .ToLower(),
                exchange.Durable,
                exchange.AutoDelete,
                exchange.Arguments);

            _model.BasicPublish(
                exchange.Name,
                routingKey == null
                    ? string.Empty
                    : routingKey.ToLower(),
                mandatory,
                properties,
                exchange
                    .Proto
                    .MessageToBytes<TMessage, TExchange>(
                        message));

            return Task.CompletedTask;
        }
    }
}
