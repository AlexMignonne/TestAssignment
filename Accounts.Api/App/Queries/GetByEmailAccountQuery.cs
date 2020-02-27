using Accounts.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Accounts.Api.App.Queries
{
    public sealed class GetByEmailAccountQuery
        : Command,
            IRequest<AccountDto>
    {
        public GetByEmailAccountQuery(
            string correlationToken,
            string email)
            : base(correlationToken)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
