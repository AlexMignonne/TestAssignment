using Accounts.SharedLibrary.ViewModels;

namespace Accounts.Api.DataTransferObjects
{
    public sealed class AccountDto
    {
        public AccountDto(
            AccountStatusEnum accountStatus,
            string email,
            int provinceId,
            AddressDto address)
        {
            Id = default;
            AccountStatus = accountStatus;
            Email = email;
            ProvinceId = provinceId;
            Address = address;
        }

        public AccountDto(
            int id,
            AccountStatusEnum accountStatus,
            string email,
            int provinceId,
            AddressDto address)
        {
            Id = id;
            AccountStatus = accountStatus;
            Email = email;
            ProvinceId = provinceId;
            Address = address;
        }

        public int Id { get; }
        public AccountStatusEnum AccountStatus { get; }
        public string Email { get; }
        public int ProvinceId { get; }
        public AddressDto Address { get; }
    }
}
