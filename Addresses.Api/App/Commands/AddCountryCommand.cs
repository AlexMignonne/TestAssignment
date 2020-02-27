using Addresses.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands
{
    public sealed class AddCountryCommand
        : Command,
            IRequest<CountryDto>
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
