using System.Collections.Generic;
using CommonLibrary.Messages;
using MediatR;

namespace Accounts.Api.App.Queries.GetListAccount
{
    public sealed class GetListAccountQuery
        : Command,
            IRequest<IEnumerable<GetListAccountDto>?>
    {
        public GetListAccountQuery(
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
