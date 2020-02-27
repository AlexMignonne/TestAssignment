using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Service.IntegrationEventHandlers.Addresses.Province
{
    public sealed class AddProvinceIntegrationEventHandler
        : RabbitHandler<
            AddedProvinceIntegrationEvent,
            AddedProvinceExchange>
    {
        public AddProvinceIntegrationEventHandler(
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
