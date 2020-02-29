using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq.Publisher;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Province
{
    public sealed class RemovedProvinceDomainEventHandler
        : INotificationHandler<RemoveProvinceDomainEvent>
    {
        private readonly ILogger<RemovedProvinceDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public RemovedProvinceDomainEventHandler(
            ILogger<RemovedProvinceDomainEventHandler> logger,
            IRabbitPublisher rabbitPublisher)
        {
            _logger = logger;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task Handle(
            RemoveProvinceDomainEvent notification,
            CancellationToken token)
        {
            var removeProvinceIntegrationEvent =
                new RemovedProvinceIntegrationEvent(
                    notification.CorrelationToken,
                    notification.Province.Id);

            await _rabbitPublisher
                .Publish<
                    RemovedProvinceIntegrationEvent,
                    RemovedProvinceExchange>(
                    removeProvinceIntegrationEvent);
        }
    }
}
