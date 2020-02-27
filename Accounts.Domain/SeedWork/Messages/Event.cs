using MediatR;

namespace Accounts.Domain.SeedWork.Messages
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
