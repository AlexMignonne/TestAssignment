using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq.Publisher;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Country
{
    public sealed class AddedCountryDomainEventHandler
        : INotificationHandler<AddCountryDomainEvent>
    {
        private readonly ILogger<AddedCountryDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public AddedCountryDomainEventHandler(
            ILogger<AddedCountryDomainEventHandler> logger,
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
