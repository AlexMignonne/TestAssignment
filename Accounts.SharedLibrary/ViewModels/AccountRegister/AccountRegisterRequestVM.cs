namespace Accounts.SharedLibrary.ViewModels.AccountRegister
{
    public sealed class AccountRegisterRequestVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProvinceId { get; set; }
        public bool? Agree { get; set; }
    }
}
