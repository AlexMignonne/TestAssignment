using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Service.IntegrationEventHandlers.Addresses.Province
{
    public sealed class RemoveProvinceIntegrationEventHandler
        : RabbitHandler<
            RemovedProvinceIntegrationEvent,
            RemovedProvinceExchange>
    {
        public RemoveProvinceIntegrationEventHandler(
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
