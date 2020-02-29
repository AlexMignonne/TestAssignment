using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class RemovedCountryExchange
        : RabbitExchange
    {
        public RemovedCountryExchange()
            : base(
                new JsonProto(),
                "addresses.country_removed",
                RabbitExchangeType.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
