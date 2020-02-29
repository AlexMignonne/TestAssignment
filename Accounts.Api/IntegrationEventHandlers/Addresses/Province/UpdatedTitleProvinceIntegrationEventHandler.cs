using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq.Handler;
using RabbitMQ.Client.Events;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Province
{
    public sealed class UpdatedTitleProvinceIntegrationEventHandler
        : IRabbitHandler<
            UpdatedTitleProvinceIntegrationEvent,
            UpdatedTitleProvinceExchange>
    {
        public Task Receive(
            UpdatedTitleProvinceIntegrationEvent message,
            BasicDeliverEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
