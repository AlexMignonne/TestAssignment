using Accounts.SharedLibrary.ViewModels;

namespace Accounts.Api.App.Queries.GetListAccount
{
    public sealed class GetListAccountDto
    {
        public GetListAccountDto(
            int id,
            AccountStatusEnum accountStatus,
            string email,
            int provinceId)
        {
            Id = id;
            AccountStatus = accountStatus;
            Email = email;
            ProvinceId = provinceId;
        }

        public int Id { get; }
        public AccountStatusEnum AccountStatus { get; }
        public string Email { get; }
        public int ProvinceId { get; }
    }
}
