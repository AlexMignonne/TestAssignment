using System;
using Polly;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMq
{
    public sealed class RabbitEndpointConfiguration
        : IDisposable
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public RabbitEndpointConfiguration(
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            string? uri)
        {
            var isUri = Uri
                .TryCreate(
                    uri,
                    UriKind.Absolute,
                    out var factoryUri);

            if (!isUri)
                throw new UriFormatException(
                    nameof(uri));

            var factory = new ConnectionFactory
            {
                DispatchConsumersAsync = true,
                Uri = factoryUri
            };

            // TODO add to conf.
            var waitAndRetry = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    new[]
                    {
                        TimeSpan.FromSeconds(10),
                        TimeSpan.FromSeconds(20),
                        TimeSpan.FromSeconds(30)
                    });

            // TODO 
            waitAndRetry
                .ExecuteAsync(
                    async () =>
                    {
                        Connection = factory
                            .CreateConnection();

                        Model = Connection
                            .CreateModel();
                    })
                .Wait();

            AppDomain
                .CurrentDomain
                .ProcessExit += (
                    sender,
                    args) =>
                Dispose();

            Console
                .CancelKeyPress += (
                sender,
                args) =>
            {
                Dispose();
                args.Cancel = true;
            };
        }

        public IConnection Connection { get; private set; }
        public IModel Model { get; private set; }

        public void Dispose()
        {
            Model?.Close();
            Model?.Dispose();
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}
