using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.SeedWork.Messages;

namespace Accounts.Domain.Events
{
    public sealed class RegisteredUserDomainEvent
        : Event
    {
        internal RegisteredUserDomainEvent(
            string correlationId,
            AccountDomain account)
            : base(correlationId)
        {
            AccountDomain = account;
        }

        public AccountDomain AccountDomain { get; }
    }
}
