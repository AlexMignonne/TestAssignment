using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class RemovedCountryExchange
        : RabbitExchange
    {
        public RemovedCountryExchange()
            : base(
                new JsonProto(),
                "addresses.country_removed",
                RabbitExchangeTypeEnum.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
