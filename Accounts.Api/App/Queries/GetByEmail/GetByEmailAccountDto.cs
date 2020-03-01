using Accounts.SharedLibrary.ViewModels;

namespace Accounts.Api.App.Queries.GetByEmail
{
    public sealed class GetByEmailAccountDto
    {
        public GetByEmailAccountDto(
            int id,
            AccountStatusEnum accountStatus,
            string email,
            GetByEmailAccountAddressDto? address)
        {
            Id = id;
            AccountStatus = accountStatus;
            Email = email;
            Address = address;
        }

        public int Id { get; }
        public AccountStatusEnum AccountStatus { get; }
        public string Email { get; }
        public GetByEmailAccountAddressDto? Address { get; }
    }
}
