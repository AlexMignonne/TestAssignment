using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Commands.AddProvince
{
    public sealed class AddProvinceCommand
        : Command,
            IRequest<AddProvinceDto?>
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
