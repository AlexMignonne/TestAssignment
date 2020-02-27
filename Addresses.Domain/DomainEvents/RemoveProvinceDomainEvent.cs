using Addresses.Domain.AggregatesModel.Province;
using Addresses.Domain.SeedWork.Messages;

namespace Addresses.Domain.DomainEvents
{
    public sealed class RemoveProvinceDomainEvent
        : Event
    {
        public RemoveProvinceDomainEvent(
            string correlationId,
            ProvinceDomain province)
            : base(correlationId)
        {
            Province = province;
        }

        public ProvinceDomain Province { get; }
    }
}
