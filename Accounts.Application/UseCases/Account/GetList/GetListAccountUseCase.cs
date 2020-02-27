using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Queries;
using Accounts.Api.DataTransferObjects;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.SharedLibrary.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.UseCases.Account.GetList
{
    public sealed class GetListAccountUseCase
        : IRequestHandler<
            GetListAccountQuery,
            IEnumerable<AccountDto>>
    {
        private readonly IAccountQueries _accountQueries;
        private readonly ILogger<GetListAccountUseCase> _logger;

        public GetListAccountUseCase(
            ILogger<GetListAccountUseCase> logger,
            IAccountQueries accountQueries)
        {
            _logger = logger;
            _accountQueries = accountQueries;
        }

        public async Task<IEnumerable<AccountDto>> Handle(
            GetListAccountQuery request,
            CancellationToken token)
        {
            var accountDomains = await _accountQueries
                .GetList(
                    request.CorrelationToken,
                    request.Page,
                    request.Amount,
                    token);

            return accountDomains
                .Select(
                    _ => new AccountDto(
                        _.Id,
                        (AccountStatusEnum) _.AccountStatus.Id,
                        _.Email,
                        _.ProvinceId,
                        null));
        }
    }
}
