using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Queries;
using Accounts.Api.DataTransferObjects;
using Accounts.Application.Exceptions;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.AggregatesModel.Address;
using Accounts.Domain.Types;
using Accounts.SharedLibrary.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.UseCases.Account
{
    public sealed class GetByEmailAccountUseCase
        : IRequestHandler<
            GetByEmailAccountQuery,
            AccountDto>
    {
        private readonly IAccountQueries _accountQueries;
        private readonly IAddressQueries _addressQueries;
        private readonly ILogger<GetByEmailAccountUseCase> _logger;

        public GetByEmailAccountUseCase(
            ILogger<GetByEmailAccountUseCase> logger,
            IAccountQueries accountQueries,
            IAddressQueries addressQueries)
        {
            _logger = logger;
            _accountQueries = accountQueries;
            _addressQueries = addressQueries;
        }

        public async Task<AccountDto?> Handle(
            GetByEmailAccountQuery request,
            CancellationToken token)
        {
            if (!await _accountQueries
                .IsExistByEmail(
                    request.CorrelationToken,
                    request.Email,
                    token))
                return null;

            var accountDomain = await _accountQueries
                .GetByEmail(
                    request.CorrelationToken,
                    request.Email,
                    token);

            if (Equals(
                accountDomain.AccountStatus,
                AccountStatusType.AddressVerificationRequired))
                return new AccountDto(
                    accountDomain.Id,
                    (AccountStatusEnum) accountDomain.Id,
                    accountDomain.Email,
                    accountDomain.ProvinceId,
                    null);

            var address = await _addressQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    accountDomain.ProvinceId,
                    token);

            if (address == null)
                throw new AppException(
                    "An error has occurred. " +
                    "We are working on a solution to it. " +
                    "You can find out the status by ticket: " +
                    $"{request.CorrelationToken}");

            return new AccountDto(
                accountDomain.Id,
                (AccountStatusEnum) accountDomain.Id,
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
