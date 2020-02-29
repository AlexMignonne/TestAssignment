using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class AddedCountryExchange
        : RabbitExchange
    {
        public AddedCountryExchange()
            : base(
                new JsonProto(),
                "addresses.country_added",
                RabbitExchangeType.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
