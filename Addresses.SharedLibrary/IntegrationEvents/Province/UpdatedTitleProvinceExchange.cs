using CommonLibrary.RabbitMq;
using CommonLibrary.RabbitMq.Declare;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class UpdatedTitleProvinceExchange
        : RabbitExchange
    {
        public UpdatedTitleProvinceExchange()
            : base(
                new JsonProto(),
                "addresses.province_updated_title",
                RabbitExchangeTypeEnum.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
