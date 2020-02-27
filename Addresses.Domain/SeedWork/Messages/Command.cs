using MediatR;

namespace Addresses.Domain.SeedWork.Messages
{
    public abstract class Command<TResponse>
        : Message,
            IRequest<TResponse>
    {
        protected Command(
            string correlationToken)
            : base(correlationToken)
        {
        }
    }

    public abstract class Command
        : Message,
            IRequest
    {
        protected Command(
            string correlationToken)
            : base(correlationToken)
        {
        }
    }
}
