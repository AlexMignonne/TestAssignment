using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Province
{
    public sealed class AddedProvinceExchange
        : RabbitExchange
    {
        public AddedProvinceExchange()
            : base(
                new JsonProto(),
                "addresses.province_added",
                RabbitExchangeType.Fanout,
                true,
                false,
                null)
        {
        }
    }
}
