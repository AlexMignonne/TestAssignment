namespace Accounts.SharedLibrary.ViewModels.GetByEmail
{
    public sealed class GetByEmailAccountResponseVM
    {
        public AccountStatusEnum AccountStatus { get; set; }
        public string Email { get; set; }
        public GetByEmailAccountAddressResponseVM Address { get; set; }
    }
}
