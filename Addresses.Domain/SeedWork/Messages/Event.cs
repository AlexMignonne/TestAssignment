using MediatR;

namespace Addresses.Domain.SeedWork.Messages
{
    public abstract class Event
        : Message,
            INotification
    {
        protected Event(
            string correlationId)
            : base(correlationId)
        {
        }
    }
}
