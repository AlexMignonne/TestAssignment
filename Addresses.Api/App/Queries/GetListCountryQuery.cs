using System.Collections.Generic;
using Addresses.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries
{
    public sealed class GetListCountryQuery
        : Command,
            IRequest<IEnumerable<CountryDto>>
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
