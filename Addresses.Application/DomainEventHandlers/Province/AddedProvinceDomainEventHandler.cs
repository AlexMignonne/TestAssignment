using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.DomainEvents;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq.Publisher;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.DomainEventHandlers.Province
{
    public sealed class AddedProvinceDomainEventHandler
        : INotificationHandler<AddProvinceDomainEvent>
    {
        private readonly ILogger<AddedProvinceDomainEventHandler> _logger;
        private readonly IRabbitPublisher _rabbitPublisher;

        public AddedProvinceDomainEventHandler(
            ILogger<AddedProvinceDomainEventHandler> logger,
            IRabbitPublisher rabbitPublisher)
        {
            _logger = logger;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task Handle(
            AddProvinceDomainEvent notification,
            CancellationToken token)
        {
            var addProvinceIntegrationEvent =
                new AddedProvinceIntegrationEvent(
                    notification.CorrelationToken,
                    notification.Province.Id,
                    notification.Province.Title);

            await _rabbitPublisher
                .Publish<
                    AddedProvinceIntegrationEvent,
                    AddedProvinceExchange>(
                    addProvinceIntegrationEvent);
        }
    }
}
