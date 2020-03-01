using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Queries.GetByEmail;
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
            GetByEmailAccountDto?>
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

        public async Task<GetByEmailAccountDto?> Handle(
            GetByEmailAccountQuery request,
            CancellationToken token)
        {
            var accountDomain = await _accountQueries
                .GetByEmail(
                    request.CorrelationToken,
                    request.Email,
                    token);

            if (accountDomain == null)
                return null;

            if (Equals(
                accountDomain
                    .AccountStatus,
                AccountStatusType.AddressVerificationRequired))
                return new GetByEmailAccountDto(
                    accountDomain.Id,
                    (AccountStatusEnum) accountDomain.Id,
                    accountDomain.Email,
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

            return new GetByEmailAccountDto(
                accountDomain.Id,
                (AccountStatusEnum) accountDomain.Id,
                accountDomain.Email,
                new GetByEmailAccountAddressDto(
                    address.CountryId,
                    address.CountryTitle,
                    address.ProvinceId,
                    address.ProvinceTitle));
        }
    }
}
