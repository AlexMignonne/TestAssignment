using CommonLibrary.RabbitMq.Messages;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class AddedProvinceIntegrationEvent
        : Message<AddedProvinceExchange>
    {
        public AddedProvinceIntegrationEvent(
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
