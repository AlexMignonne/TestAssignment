using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Country
{
    public sealed class AddedCountryIntegrationEventHandler
        : RabbitHandler<
            AddedCountryIntegrationEvent,
            AddedCountryExchange>
    {
        public AddedCountryIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange)
            : base(
                endpointConfiguration,
                exchange,
                "country_added.account_service")
        {
        }

        public override Task Receive(
            AddedCountryIntegrationEvent message,
            string correlationToken)
        {
            throw new NotImplementedException();
        }
    }
}
