using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Commands;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.Types;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.UseCases.Account
{
    public sealed class CountryRemoveUseCase
        : AsyncRequestHandler<CountryRemoveCommand>
    {
        private readonly IAccountCommand _accountCommand;
        private readonly IAccountQueries _accountQueries;
        private readonly ILogger<CountryRemoveUseCase> _logger;

        public CountryRemoveUseCase(
            ILogger<CountryRemoveUseCase> logger,
            IAccountCommand accountCommand,
            IAccountQueries accountQueries)
        {
            _logger = logger;
            _accountCommand = accountCommand;
            _accountQueries = accountQueries;
        }

        protected override async Task Handle(
            CountryRemoveCommand request,
            CancellationToken token)
        {
            var provinceIds = await _accountQueries
                .GetByProvinceIds(
                    request.CorrelationToken,
                    request.ProvinceIds,
                    token);

            if (provinceIds == null)
                return;

            foreach (var accountDomain in provinceIds)
                accountDomain
                    .ChangeStatus(
                        AccountStatusType.AddressVerificationRequired);

            await _accountCommand
                .UnitOfWork
                .SaveEntitiesAsync(token);
        }
    }
}
