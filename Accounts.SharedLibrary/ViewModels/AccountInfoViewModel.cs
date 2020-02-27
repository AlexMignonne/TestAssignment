namespace Accounts.SharedLibrary.ViewModels
{
    public sealed class AccountInfoViewModel
    {
        public AccountStatusEnum AccountStatus { get; set; }
        public string Email { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
