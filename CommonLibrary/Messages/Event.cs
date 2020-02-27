using MediatR;

namespace CommonLibrary.Messages
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
