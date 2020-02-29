using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class RemovedProvinceExchange
        : RabbitExchange
    {
        public RemovedProvinceExchange()
            : base(
                new JsonProto(),
                "addresses.province_removed",
                RabbitExchangeType.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
