using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.RabbitMq.RabbitMqExceptions;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommonLibrary.RabbitMq.Handler
{
    internal sealed class RabbitHandler<TRabbitHandler, TMessage, TExchange>
        where TRabbitHandler : IRabbitHandler<TMessage, TExchange>
        where TMessage : Message<TExchange>
        where TExchange : RabbitExchange, new()
    {
        public RabbitHandler(
            IServiceProvider serviceProvider,
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange,
            string queue,
            bool durable = true,
            bool exclusive = false,
            bool autoDelete = false,
            bool autoAct = false,
            IReadOnlyCollection<string>? routingKeys = null,
            IDictionary<string, object>? arguments = null)
        {
            if (exchange == null)
                throw new ArgumentException(
                    $"Message {typeof(TMessage)} not registered in exchange.");

            if (exchange.Type == RabbitExchangeType.Fanout &&
                routingKeys != null &&
                routingKeys.Any())
                throw new ArgumentException(
                    "Fanout cannot use routing key.");

            if (string.IsNullOrWhiteSpace(queue))
                throw new ArgumentException(
                    "Queue cannot be empty.");

            var model = endpointConfiguration.Model;

            model
                .ExchangeDeclare(
                    exchange.Name,
                    exchange
                        .Type
                        .ToString()
                        .ToLower(),
                    exchange.Durable,
                    exchange.AutoDelete,
                    exchange.Arguments);

            model
                .QueueDeclare(
                    queue,
                    durable,
                    exclusive,
                    autoDelete,
                    arguments);

            if (routingKeys == null ||
                !routingKeys.Any())
                model
                    .QueueBind(
                        queue,
                        exchange.Name,
                        string.Empty,
                        arguments);
            else
                foreach (var routingKey in routingKeys)
                    model
                        .QueueBind(
                            queue,
                            exchange.Name,
                            routingKey,
                            arguments);

            if (!autoAct)
                model
                    .BasicQos(
                        0,
                        1,
                        false);

            var consumer = new AsyncEventingBasicConsumer(
                model);

            consumer
                .Received += async (
                obj,
                args) =>
            {
                var message = exchange
                    .Proto
                    .BytesToMessage<TMessage, TExchange>(
                        args.Body);

                try
                {
                    var rabbitHandler = serviceProvider
                        .GetService<TRabbitHandler>();

                    rabbitHandler
                        .Receive(
                            message,
                            args)
                        .Wait();
                }
                catch (Exception e)
                {
                    throw new RabbitMqException(
                        e.Message,
                        e);
                }

                if (!autoAct)
                    model.BasicAck(
                        args.DeliveryTag,
                        false);

                await Task.Yield();
            };

            model
                .BasicConsume(
                    queue,
                    autoAct,
                    consumer);
        }
    }
}
