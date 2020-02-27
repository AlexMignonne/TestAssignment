using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Province
{
    public sealed class RemoveProvinceDomainEventHandler
        : INotificationHandler<RemoveProvinceDomainEvent>
    {
        private readonly ILogger<RemoveProvinceDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public RemoveProvinceDomainEventHandler(
            ILogger<RemoveProvinceDomainEventHandler> logger,
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
