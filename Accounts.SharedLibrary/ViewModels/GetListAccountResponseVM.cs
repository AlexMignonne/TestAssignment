namespace Accounts.SharedLibrary.ViewModels
{
    public sealed class GetListAccountResponseVM
    {
        public AccountStatusEnum AccountStatus { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
    }
}
