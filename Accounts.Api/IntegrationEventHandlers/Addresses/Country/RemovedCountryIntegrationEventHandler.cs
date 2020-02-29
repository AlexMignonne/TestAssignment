using System.Threading.Tasks;
using Accounts.Api.App.Commands;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq.Handler;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Country
{
    public sealed class RemovedCountryIntegrationEventHandler
        : IRabbitHandler<
            RemovedCountryIntegrationEvent,
            RemovedCountryExchange>
    {
        private readonly ILogger<RemovedCountryIntegrationEventHandler> _logger;
        private readonly IMediator _mediator;

        public RemovedCountryIntegrationEventHandler(
            ILogger<RemovedCountryIntegrationEventHandler> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Receive(
            RemovedCountryIntegrationEvent message,
            BasicDeliverEventArgs args)
        {
            await _mediator
                .Send(
                    new CountryRemoveCommand(
                        args.BasicProperties.CorrelationId,
                        message.Id,
                        message.ProvinceIds));
        }
    }
}
