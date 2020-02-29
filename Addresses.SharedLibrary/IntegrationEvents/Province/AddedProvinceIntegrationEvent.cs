using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class AddedProvinceIntegrationEvent
        : Message<AddedProvinceExchange>
    {
        public AddedProvinceIntegrationEvent(
            string correlationToken,
            int id,
            string title)
        {
            CorrelationToken = correlationToken;
            Id = id;
            Title = title;
        }

        public string CorrelationToken { get; }
        public int Id { get; }
        public string Title { get; }
    }
}
