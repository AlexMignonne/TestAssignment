using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class UpdatedTitleProvinceIntegrationEvent
        : Message<UpdatedTitleProvinceExchange>
    {
        public UpdatedTitleProvinceIntegrationEvent(
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
