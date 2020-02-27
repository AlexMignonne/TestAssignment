using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Service.IntegrationEventHandlers.Addresses.Province
{
    public sealed class UpdateTitleProvinceIntegrationEventHandler
        : RabbitHandler<
            UpdatedTitleProvinceIntegrationEvent,
            UpdatedTitleProvinceExchange>
    {
        public UpdateTitleProvinceIntegrationEventHandler(
            RabbitEndpointConfiguration endpointConfiguration,
            RabbitExchange exchange)
            : base(
                endpointConfiguration,
                exchange,
                "province_updated_title.account_service")
        {
        }

        public override Task Receive(
            UpdatedTitleProvinceIntegrationEvent message,
            string correlationToken)
        {
            throw new NotImplementedException();
        }
    }
}
