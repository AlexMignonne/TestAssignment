using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Service.IntegrationEventHandlers.Addresses.Country
{
    public sealed class RemoveCountryIntegrationEventHandler
        : RabbitHandler<
            RemovedCountryIntegrationEvent,
            RemovedCountryExchange>
    {
        public RemoveCountryIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange)
            : base(
                endpointConfiguration,
                exchange,
                "country_removed.account_service")
        {
        }

        public override Task Receive(
            RemovedCountryIntegrationEvent message,
            string correlationToken)
        {
            throw new NotImplementedException();
        }
    }
}
