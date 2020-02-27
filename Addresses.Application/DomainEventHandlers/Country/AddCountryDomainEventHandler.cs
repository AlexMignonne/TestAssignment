using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Country
{
    public sealed class AddCountryDomainEventHandler
        : INotificationHandler<AddCountryDomainEvent>
    {
        private readonly ILogger<AddCountryDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public AddCountryDomainEventHandler(
            ILogger<AddCountryDomainEventHandler> logger,
            IRabbitPublisher rabbitPublisher)
        {
            _logger = logger;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task Handle(
            AddCountryDomainEvent notification,
            CancellationToken token)
        {
            var addCountryIntegrationEvent =
                new AddedCountryIntegrationEvent(
                    notification.CorrelationToken,
                    notification.Country.Id,
                    notification.Country.Title);

            await _rabbitPublisher
                .Publish<
                    AddedCountryIntegrationEvent,
                    AddedCountryExchange>(
                    addCountryIntegrationEvent);
        }
    }
}
