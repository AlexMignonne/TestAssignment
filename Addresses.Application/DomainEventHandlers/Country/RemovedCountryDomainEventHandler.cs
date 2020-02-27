using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Country
{
    public sealed class RemovedCountryDomainEventHandler
        : INotificationHandler<RemoveCountryDomainEvent>
    {
        private readonly ILogger<RemovedCountryDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public RemovedCountryDomainEventHandler(
            ILogger<RemovedCountryDomainEventHandler> logger,
            IRabbitPublisher rabbitPublisher)
        {
            _logger = logger;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task Handle(
            RemoveCountryDomainEvent notification,
            CancellationToken token)
        {
            var removeCountryIntegrationEvent =
                new RemovedCountryIntegrationEvent(
                    notification
                        .CorrelationToken,
                    notification
                        .Country
                        .Id,
                    notification
                        .Country
                        .Provinces
                        .Select(_ => _.Id));

            await _rabbitPublisher
                .Publish<
                    RemovedCountryIntegrationEvent,
                    RemovedCountryExchange>(
                    removeCountryIntegrationEvent);
        }
    }
}
