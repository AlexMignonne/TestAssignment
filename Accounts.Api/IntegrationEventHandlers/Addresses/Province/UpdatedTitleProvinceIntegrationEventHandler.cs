using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Province
{
    public sealed class UpdatedTitleProvinceIntegrationEventHandler
        : RabbitHandler<
            UpdatedTitleProvinceIntegrationEvent,
            UpdatedTitleProvinceExchange>
    {
        public UpdatedTitleProvinceIntegrationEventHandler(
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
