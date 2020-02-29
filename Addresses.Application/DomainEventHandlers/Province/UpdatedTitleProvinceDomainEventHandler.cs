using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq.Publisher;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Province
{
    public sealed class UpdatedTitleProvinceDomainEventHandler
        : INotificationHandler<UpdateTitleProvinceDomainEvent>
    {
        private readonly ILogger<UpdatedTitleProvinceDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public UpdatedTitleProvinceDomainEventHandler(
            ILogger<UpdatedTitleProvinceDomainEventHandler> logger,
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
