using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Province
{
    public sealed class RemovedProvinceIntegrationEventHandler
        : RabbitHandler<
            RemovedProvinceIntegrationEvent,
            RemovedProvinceExchange>
    {
        public RemovedProvinceIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange)
            : base(
                endpointConfiguration,
                exchange,
                "province_removed.account_service")
        {
        }

        public override Task Receive(
            RemovedProvinceIntegrationEvent message,
            string correlationToken)
        {
            throw new NotImplementedException();
        }
    }
}
