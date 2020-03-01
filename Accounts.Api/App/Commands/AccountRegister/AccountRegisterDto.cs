using Accounts.SharedLibrary.ViewModels;

namespace Accounts.Api.App.Commands.AccountRegister
{
    public sealed class AccountRegisterDto
    {
        public AccountRegisterDto(
            int id,
            AccountStatusEnum accountStatus,
            string email,
            AccountRegisterAddressDto address)
        {
            Id = id;
            AccountStatus = accountStatus;
            Email = email;
            Address = address;
        }

        public int Id { get; }
        public AccountStatusEnum AccountStatus { get; }
        public string Email { get; }
        public AccountRegisterAddressDto Address { get; }
    }
}
