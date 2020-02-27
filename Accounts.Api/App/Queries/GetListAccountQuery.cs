using System.Collections.Generic;
using Accounts.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Accounts.Api.App.Queries
{
    public sealed class GetListAccountQuery
        : Command,
            IRequest<IEnumerable<AccountDto>>
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
