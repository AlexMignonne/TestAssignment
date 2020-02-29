using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands.UpdateTitleCountry
{
    public sealed class UpdateTitleCountryCommand
        : Command,
            IRequest<UpdateTitleCountryDto?>
    {
        public UpdateTitleCountryCommand(
            string correlationToken,
            int id,
            string title)
            : base(correlationToken)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }
        public string Title { get; }
    }
}
