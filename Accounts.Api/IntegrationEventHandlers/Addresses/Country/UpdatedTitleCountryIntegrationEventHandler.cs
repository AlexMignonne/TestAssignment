using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq.Handler;
using RabbitMQ.Client.Events;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Country
{
    public sealed class UpdatedTitleCountryIntegrationEventHandler
        : IRabbitHandler<
            UpdatedTitleCountryIntegrationEvent,
            UpdatedTitleCountryExchange>
    {
        public Task Receive(
            UpdatedTitleCountryIntegrationEvent message,
            BasicDeliverEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
