using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.SeedWork.Messages;

namespace Addresses.Domain.DomainEvents
{
    public sealed class RemoveCountryDomainEvent
        : Event
    {
        public RemoveCountryDomainEvent(
            string correlationId,
            CountryDomain country)
            : base(correlationId)
        {
            Country = country;
        }

        public CountryDomain Country { get; }
    }
}
