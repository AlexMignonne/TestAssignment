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
            : base(
                correlationToken)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }
        public string Title { get; }
    }
}
