using Accounts.Api.DataTransferObjects;
using Accounts.SharedLibrary.ViewModels;
using CommonLibrary.Messages;
using MediatR;

namespace Accounts.Api.App.Commands
{
    public sealed class RegisterAccountCommand
        : Command,
            IRequest<AccountDto>
    {
        public RegisterAccountCommand(
            string correlationToken,
            AccountStatusEnum accountStatus,
            string email,
            string password,
            int provinceId,
            bool? agree)
            : base(correlationToken)
        {
            AccountStatus = accountStatus;
            Email = email;
            Password = password;
            ProvinceId = provinceId;
            Agree = agree;
        }

        public AccountStatusEnum AccountStatus { get; }
        public string Email { get; }
        public string Password { get; }
        public int ProvinceId { get; }
        public bool? Agree { get; }
    }
}
