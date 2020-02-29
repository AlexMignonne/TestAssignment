using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class RemovedProvinceIntegrationEvent
        : Message<RemovedProvinceExchange>
    {
        public RemovedProvinceIntegrationEvent(
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
