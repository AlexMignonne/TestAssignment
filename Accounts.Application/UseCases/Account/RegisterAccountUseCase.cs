using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Commands;
using Accounts.Api.DataTransferObjects;
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
            RegisterAccountCommand,
            AccountDto>
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

        public async Task<AccountDto?> Handle(
            RegisterAccountCommand request,
            CancellationToken token)
        {
            if (await _accountQueries
                .IsExistByEmail(
                    request.CorrelationToken,
                    request.Email,
                    token))
                throw new AppException(
                    $"Account on the mail {request.Email} is already registered");

            if (!await _addressQueries
                .IsExistByProvinceId(
                    request.CorrelationToken,
                    request.ProvinceId,
                    token))
                throw new AppException(
                    $"Province with id {request.ProvinceId} not exist");

            var address = await _addressQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    request.ProvinceId,
                    token);

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

            await _accountCommand
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new AccountDto(
                accountDomain.Id,
                (AccountStatusEnum) accountDomain.AccountStatus.Id,
                accountDomain.Email,
                accountDomain.ProvinceId,
                new AddressDto(
                    address.CountryId,
                    address.CountryTitle,
                    address.ProvinceId,
                    address.ProvinceTitle));
        }
    }
}
