using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class AddedProvinceExchange
        : RabbitExchange
    {
        public AddedProvinceExchange()
            : base(
                new JsonProto(),
                "addresses.province_added",
                RabbitExchangeTypeEnum.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
