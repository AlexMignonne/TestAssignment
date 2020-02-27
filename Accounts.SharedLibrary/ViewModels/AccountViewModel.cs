namespace Accounts.SharedLibrary.ViewModels
{
    public sealed class AccountViewModel
    {
        public AccountStatusEnum AccountStatus { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
    }
}
