using Addresses.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands
{
    public sealed class AddProvinceCommand
        : Command,
            IRequest<ProvinceDto>
    {
        public AddProvinceCommand(
            string correlationToken,
            int countryId,
            string title)
            : base(correlationToken)
        {
            CountryId = countryId;
            Title = title;
        }

        public int CountryId { get; }
        public string Title { get; }
    }
}
