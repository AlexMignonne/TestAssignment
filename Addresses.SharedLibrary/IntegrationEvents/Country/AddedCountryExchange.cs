using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class AddedCountryExchange
        : RabbitExchange
    {
        public AddedCountryExchange()
            : base(
                new JsonProto(),
                "addresses.country_added",
                RabbitExchangeTypeEnum.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
