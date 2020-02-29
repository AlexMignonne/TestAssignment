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
        {
            CorrelationToken = correlationToken;
            Id = id;
            ProvinceIds = provinceIds;
        }

        public string CorrelationToken { get; }
        public int Id { get; }
        public IEnumerable<int> ProvinceIds { get; }
    }
}
