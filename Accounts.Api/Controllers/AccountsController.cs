using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Commands;
using Accounts.Api.App.Queries;
using Accounts.Api.DataTransferObjects;
using Accounts.SharedLibrary.ViewModels;
using CommonLibrary.RequestInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController
        : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediator;

        public AccountsController(
            ILogger<AccountsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetByEmail(
            [FromRoute] string email,
            CancellationToken token = default)
        {
            var accountDomain = await _mediator
                .Send<AccountDto>(
                    new GetByEmailAccountQuery(
                        RequestInfo.CorrelationToken,
                        email),
                    token);

            return accountDomain == null
                ? (ActionResult) NotFound()
                : Ok(
                    new AccountInfoViewModel
                    {
                        AccountStatus = accountDomain
                            .AccountStatus,
                        Email = accountDomain.Email,
                        Address = new AddressViewModel
                        {
                            CountryId = accountDomain.Address.CountryId,
                            CountryTitle = accountDomain.Address.CountryTitle,
                            ProvinceId = accountDomain.Address.ProvinceId,
                            ProvinceTitle = accountDomain.Address.ProvinceTitle
                        }
                    });
        }

        [HttpGet]
        public async Task<ActionResult?> GetList(
            [FromQuery] int page = 1,
            [FromQuery] int amount = 10,
            CancellationToken token = default)
        {
            var accounts = await _mediator
                .Send<IEnumerable<AccountDto>>(
                    new GetListAccountQuery(
                        RequestInfo.CorrelationToken,
                        page,
                        amount),
                    token);

            var accountDomains = accounts
                .ToList();

            return !accountDomains.Any()
                ? (ActionResult) NoContent()
                : Ok(
                    accountDomains
                        .Select(
                            _ => new AccountViewModel
                            {
                                AccountStatus = _.AccountStatus,
                                Email = _.Email,
                                ProvinceId = _.ProvinceId
                            }));
        }

        [HttpPost]
        public async Task<ActionResult?> Register(
            [FromBody] AccountRegisterViewModel viewModel,
            CancellationToken token = default)
        {
            var countryDomain = await _mediator
                .Send<AccountDto>(
                    new RegisterAccountCommand(
                        RequestInfo.CorrelationToken,
                        AccountStatusEnum.Active,
                        viewModel.Email,
                        viewModel.Password,
                        viewModel.ProvinceId,
                        viewModel.Agree),
                    token);

            if (countryDomain == null)
                return BadRequest("");

            return CreatedAtAction(
                nameof(GetByEmail),
                new
                {
                    countryDomain.Email
                },
                new AccountInfoViewModel
                {
                    AccountStatus = countryDomain
                        .AccountStatus,
                    Email = countryDomain
                        .Email,
                    Address = new AddressViewModel
                    {
                        CountryId = countryDomain
                            .Address
                            .CountryId,
                        CountryTitle = countryDomain
                            .Address
                            .CountryTitle,
                        ProvinceId = countryDomain
                            .Address
                            .ProvinceId,
                        ProvinceTitle = countryDomain
                            .Address
                            .ProvinceTitle
                    }
                });
        }
    }
}
