using System;
using Polly;
using RabbitMQ.Client;

namespace CommonLibrary.RabbitMq
{
    public sealed class RabbitEndpointConfiguration
        : IDisposable
    {
        public RabbitEndpointConfiguration(
            string? uri = null)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentNullException(
                    $"{nameof(uri)} in {nameof(RabbitEndpointConfiguration)}");

            var isUri = Uri.TryCreate(
                uri,
                UriKind.Absolute,
                out var factoryUri);

            if (!isUri)
                throw new UriFormatException(
                    $"{nameof(uri)} in {nameof(RabbitEndpointConfiguration)}");

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

            waitAndRetry
                .ExecuteAsync(
                    async () =>
                    {
                        Connection = factory
                            .CreateConnection();

                        Model = Connection
                            .CreateModel();
                    })
                .GetAwaiter()
                .GetResult();

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
