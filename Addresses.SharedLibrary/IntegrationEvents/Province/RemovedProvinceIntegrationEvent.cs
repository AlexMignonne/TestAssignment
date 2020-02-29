using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class RemovedProvinceIntegrationEvent
        : Message<RemovedProvinceExchange>
    {
        public RemovedProvinceIntegrationEvent(
            string correlationToken,
            int id)
        {
            CorrelationToken = correlationToken;
            Id = id;
        }

        public string CorrelationToken { get; }
        public int Id { get; }
    }
}
