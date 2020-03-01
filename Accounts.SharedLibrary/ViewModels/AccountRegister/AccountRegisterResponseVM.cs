namespace Accounts.SharedLibrary.ViewModels.AccountRegister
{
    public sealed class AccountRegisterResponseVM
    {
        public AccountStatusEnum AccountStatus { get; set; }
        public string Email { get; set; }
        public AccountRegisterAddressResponseVM Address { get; set; }
    }
}
