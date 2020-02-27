using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class RemovedProvinceExchange
        : RabbitExchange
    {
        public RemovedProvinceExchange()
            : base(
                new JsonProto(),
                "addresses.province_removed",
                RabbitExchangeTypeEnum.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
