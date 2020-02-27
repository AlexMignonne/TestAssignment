using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Province
{
    public sealed class AddedProvinceIntegrationEventHandler
        : RabbitHandler<
            AddedProvinceIntegrationEvent,
            AddedProvinceExchange>
    {
        public AddedProvinceIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange)
            : base(
                endpointConfiguration,
                exchange,
                "province_added.account_service")
        {
        }

        public override Task Receive(
            AddedProvinceIntegrationEvent message,
            string correlationToken)
        {
            throw new NotImplementedException();
        }
    }
}
