using System;
using System.Threading.Tasks;
using Accounts.Api.App.Commands;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Country
{
    public sealed class RemovedCountryIntegrationEventHandler
        : RabbitHandler<
            RemovedCountryIntegrationEvent,
            RemovedCountryExchange>
    {
        private readonly ILogger<RemovedCountryIntegrationEventHandler> _logger;
        private readonly IMediator _mediator;

        public RemovedCountryIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange,
            IServiceProvider serviceProvider)
            : base(
                endpointConfiguration,
                exchange,
                "country_removed.account_service")
        {
            _logger = serviceProvider
                .GetService<ILogger<RemovedCountryIntegrationEventHandler>>();

            _mediator = serviceProvider
                .GetService<IMediator>();
        }

        public override async Task Receive(
            RemovedCountryIntegrationEvent message,
            string correlationToken)
        {
            await _mediator
                .Send(
                    new CountryRemoveCommand(
                        correlationToken,
                        message.Id,
                        message.ProvinceIds));
        }
    }
}
