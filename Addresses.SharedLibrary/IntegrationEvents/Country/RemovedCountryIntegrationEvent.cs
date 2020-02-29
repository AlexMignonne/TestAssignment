using System.Collections.Generic;
using CommonLibrary.RabbitMq;

namespace Addresses.SharedLibrary.IntegrationEvents.Country
{
    public sealed class RemovedCountryIntegrationEvent
        : Message<RemovedCountryExchange>
    {
        public RemovedCountryIntegrationEvent(
            string correlationToken,
            int id,
            IEnumerable<int> provinceIds)
            : base(
                correlationToken)
        {
            Id = id;
            ProvinceIds = provinceIds;
        }

        public int Id { get; }
        public IEnumerable<int> ProvinceIds { get; }
    }
}
