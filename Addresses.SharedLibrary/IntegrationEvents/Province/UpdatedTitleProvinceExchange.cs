using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class UpdatedTitleProvinceExchange
        : RabbitExchange
    {
        public UpdatedTitleProvinceExchange()
            : base(
                new JsonProto(),
                "addresses.province_updated_title",
                RabbitExchangeType.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
