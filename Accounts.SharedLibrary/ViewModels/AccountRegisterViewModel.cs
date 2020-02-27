namespace Accounts.SharedLibrary.ViewModels
{
    public sealed class AccountRegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProvinceId { get; set; }
        public bool? Agree { get; set; }
    }
}
