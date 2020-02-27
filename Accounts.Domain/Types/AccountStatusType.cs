using Accounts.Domain.SeedWork;

namespace Accounts.Domain.Types
{
    public sealed class AccountStatusType
        : Enumeration
    {
        public static AccountStatusType Active =
            new AccountStatusType(
                1,
                "Active");

        public static AccountStatusType AddressVerificationRequired =
            new AccountStatusType(
                2,
                "AddressVerificationRequired");

        public AccountStatusType(
            int id,
            string name)
            : base(
                id,
                name)
        {
        }
    }
}
