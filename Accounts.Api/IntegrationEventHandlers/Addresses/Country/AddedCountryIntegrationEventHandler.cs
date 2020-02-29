using System;
using System.Threading.Tasks;
using Addresses.SharedLibrary.IntegrationEvents.Country;
using CommonLibrary.RabbitMq.Handler;
using RabbitMQ.Client.Events;

namespace Accounts.Api.IntegrationEventHandlers.Addresses.Country
{
    public sealed class AddedCountryIntegrationEventHandler
        : IRabbitHandler<
            AddedCountryIntegrationEvent,
            AddedCountryExchange>
    {
        public Task Receive(
            AddedCountryIntegrationEvent message,
            BasicDeliverEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
