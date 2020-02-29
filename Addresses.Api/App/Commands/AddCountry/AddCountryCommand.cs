using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands.AddCountry
{
    public sealed class AddCountryCommand
        : Command,
            IRequest<AddCountryDto>
    {
        public AddCountryCommand(
            string correlationToken,
            string title)
            : base(correlationToken)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
