using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands.UpdateTitleProvince
{
    public sealed class UpdateTitleProvinceCommand
        : Command,
            IRequest<UpdateTitleProvinceDto?>
    {
        public UpdateTitleProvinceCommand(
            string correlationToken,
            int id,
            string? title)
            : base(correlationToken)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }
        public string? Title { get; }
    }
}
