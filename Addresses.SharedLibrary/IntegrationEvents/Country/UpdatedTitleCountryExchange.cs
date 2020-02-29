using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class UpdatedTitleCountryExchange
        : RabbitExchange
    {
        public UpdatedTitleCountryExchange()
            : base(
                new JsonProto(),
                "addresses.country_updated_title",
                RabbitExchangeType.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
