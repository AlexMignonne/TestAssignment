using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class UpdatedTitleCountryIntegrationEvent
        : Message<UpdatedTitleCountryExchange>
    {
        public UpdatedTitleCountryIntegrationEvent(
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
