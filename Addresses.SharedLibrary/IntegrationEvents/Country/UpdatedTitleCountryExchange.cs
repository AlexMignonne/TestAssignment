using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class UpdatedTitleCountryExchange
        : RabbitExchange
    {
        public UpdatedTitleCountryExchange()
            : base(
                new JsonProto(),
                "addresses.country_updated_title",
                RabbitExchangeTypeEnum.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
