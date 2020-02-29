using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Province;
using CommonLibrary.RabbitMq.Handler;
using RabbitMQ.Client.Events;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Province
{
    public sealed class AddedProvinceIntegrationEventHandler
        : IRabbitHandler<
            AddedProvinceIntegrationEvent,
            AddedProvinceExchange>
    {
        public Task Receive(
            AddedProvinceIntegrationEvent message,
            BasicDeliverEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
