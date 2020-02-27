using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.SeedWork.Messages;

namespace Addresses.Domain.DomainEvents
{
    public sealed class AddCountryDomainEvent
        : Event
    {
        public AddCountryDomainEvent(
            string correlationId,
            CountryDomain country)
            : base(correlationId)
        {
            Country = country;
        }

        public CountryDomain Country { get; }
    }
}
