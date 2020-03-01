using CommonLibrary.Messages;
using MediatR;

namespace Accounts.Api.App.Queries.GetByEmail
{
    public sealed class GetByEmailAccountQuery
        : Command,
            IRequest<GetByEmailAccountDto?>
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
