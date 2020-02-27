using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands
{
    public sealed class RemoveProvinceCommand
        : Command,
            IRequest<bool>
    {
        public RemoveProvinceCommand(
            string correlationToken,
            int id)
            : base(correlationToken)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
