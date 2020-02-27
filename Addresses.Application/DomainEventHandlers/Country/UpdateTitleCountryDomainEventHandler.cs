using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Country
{
    public sealed class UpdateTitleCountryDomainEventHandler
        : INotificationHandler<UpdateTitleCountryDomainEvent>
    {
        private readonly ILogger<UpdateTitleCountryDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public UpdateTitleCountryDomainEventHandler(
            ILogger<UpdateTitleCountryDomainEventHandler> logger,
            IRabbitPublisher rabbitPublisher)
        {
            _logger = logger;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task Handle(
            UpdateTitleCountryDomainEvent notification,
            CancellationToken token)
        {
            var updateTitleCountryIntegrationEvent =
                new UpdatedTitleCountryIntegrationEvent(
                    notification.CorrelationToken,
                    notification.Country.Id,
                    notification.Country.Title);

            await _rabbitPublisher
                .Publish<
                    UpdatedTitleCountryIntegrationEvent,
                    UpdatedTitleCountryExchange>(
                    updateTitleCountryIntegrationEvent);
        }
    }
}
