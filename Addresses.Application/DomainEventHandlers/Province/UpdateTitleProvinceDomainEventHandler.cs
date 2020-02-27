using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Province
{
    public sealed class UpdateTitleProvinceDomainEventHandler
        : INotificationHandler<UpdateTitleProvinceDomainEvent>
    {
        private readonly ILogger<UpdateTitleProvinceDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public UpdateTitleProvinceDomainEventHandler(
            ILogger<UpdateTitleProvinceDomainEventHandler> logger,
            IRabbitPublisher rabbitPublisher)
        {
            _logger = logger;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task Handle(
            UpdateTitleProvinceDomainEvent notification,
            CancellationToken token)
        {
            var updateTitleProvinceIntegrationEvent =
                new UpdatedTitleProvinceIntegrationEvent(
                    notification.CorrelationToken,
                    notification.Province.Id,
                    notification.Province.Title);

            await _rabbitPublisher
                .Publish<
                    UpdatedTitleProvinceIntegrationEvent,
                    UpdatedTitleProvinceExchange>(
                    updateTitleProvinceIntegrationEvent);
        }
    }
}
