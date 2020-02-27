using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Service.IntegrationEventHandlers.Addresses.Country
{
    public sealed class UpdateTitleCountryIntegrationEventHandler
        : RabbitHandler<
            UpdatedTitleCountryIntegrationEvent,
            UpdatedTitleCountryExchange>
    {
        public UpdateTitleCountryIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange)
            : base(
                endpointConfiguration,
                exchange,
                "country_updated_title.account_service")
        {
        }

        public override Task Receive(
            UpdatedTitleCountryIntegrationEvent message,
            string correlationToken)
        {
            throw new NotImplementedException();
        }
    }
}
