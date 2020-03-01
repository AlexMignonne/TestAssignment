using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Commands.AccountRegister;
using Accounts.Application.Exceptions;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.AggregatesModel.Address;
using Accounts.SharedLibrary.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.UseCases.Account
{
    internal class RegisterAccountUseCase
        : IRequestHandler<
            AccountRegisterCommand,
            AccountRegisterDto?>
    {
        private readonly IAccountCommand _accountCommand;
        private readonly IAccountQueries _accountQueries;
        private readonly IAddressQueries _addressQueries;
        private readonly ILogger<RegisterAccountUseCase> _logger;

        public RegisterAccountUseCase(
            ILogger<RegisterAccountUseCase> logger,
            IAccountQueries accountQueries,
            IAccountCommand accountCommand,
            IAddressQueries addressQueries)
        {
            _logger = logger;
            _accountQueries = accountQueries;
            _accountCommand = accountCommand;
            _addressQueries = addressQueries;
        }

        public async Task<AccountRegisterDto?> Handle(
            AccountRegisterCommand request,
            CancellationToken token)
        {
            if (await _accountQueries
                .IsExistByEmail(
                    request.CorrelationToken,
                    request.Email,
                    token))
                throw new AppException(
                    $"Account on the mail {request.Email} is already registered");

            var address = await _addressQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    request.ProvinceId,
                    token);

            if (address == null)
                throw new AppException(
                    $"Province with id {request.ProvinceId} not exist");

            var accountDomain = await _accountCommand
                .Register(
                    request.CorrelationToken,
                    new AccountDomain(
                        request.CorrelationToken,
                        request.Agree,
                        request.Email,
                        request.Password,
                        request.ProvinceId),
                    token);

            if (accountDomain == null)
                return null;

            await _accountCommand
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new AccountRegisterDto(
                accountDomain.Id,
                (AccountStatusEnum)accountDomain.AccountStatus.Id,
                accountDomain.Email,
                new AccountRegisterAddressDto(
                    address.CountryId,
                    address.CountryTitle,
                    address.ProvinceId,
                    address.ProvinceTitle));
        }
    }
}
