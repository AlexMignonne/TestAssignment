using System.Collections.Generic;
using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries.GetListCountry
{
    public sealed class GetListCountryQuery
        : Command,
            IRequest<IEnumerable<GetListCountryDto>?>
    {
        public GetListCountryQuery(
            string correlationToken,
            int page = 1,
            int amount = 10)
            : base(correlationToken)
        {
            Page = page;
            Amount = amount;
        }

        public int Page { get; }
        public int Amount { get; }
    }
}
