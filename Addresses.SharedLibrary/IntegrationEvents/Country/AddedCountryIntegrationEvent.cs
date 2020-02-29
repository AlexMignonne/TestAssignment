using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class AddedCountryIntegrationEvent
        : Message<AddedCountryExchange>
    {
        public AddedCountryIntegrationEvent(
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
