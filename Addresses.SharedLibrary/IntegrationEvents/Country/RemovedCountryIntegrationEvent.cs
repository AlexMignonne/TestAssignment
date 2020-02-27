using CommonLibrary.RabbitMq.Messages;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class RemovedCountryIntegrationEvent
        : Message<RemovedCountryExchange>
    {
        public RemovedCountryIntegrationEvent(
            string correlationToken,
            int id)
            : base(
                correlationToken)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
