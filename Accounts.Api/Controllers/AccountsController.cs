using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Api.App.Commands.AccountRegister;
using Accounts.Api.App.Queries.GetByEmail;
using Accounts.Api.App.Queries.GetListAccount;
using Accounts.SharedLibrary.ViewModels;
using Accounts.SharedLibrary.ViewModels.AccountRegister;
using Accounts.SharedLibrary.ViewModels.GetByEmail;
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
            var dto = await _mediator
                .Send<GetByEmailAccountDto?>(
                    new GetByEmailAccountQuery(
                        RequestInfo.CorrelationToken,
                        email),
                    token);

            return dto == null
                ? (ActionResult) NotFound()
                : Ok(
                    new GetByEmailAccountResponseVM
                    {
                        AccountStatus = dto
                            .AccountStatus,
                        Email = dto.Email,
                        Address = dto.Address == null
                            ? null
                            : new GetByEmailAccountAddressResponseVM()
                            {
                                CountryId = dto.Address.CountryId,
                                CountryTitle = dto.Address.CountryTitle,
                                ProvinceId = dto.Address.ProvinceId,
                                ProvinceTitle = dto.Address.ProvinceTitle
                            }
                    });
        }

        [HttpGet]
        public async Task<ActionResult?> GetList(
            [FromQuery] int page = 1,
            [FromQuery] int amount = 10,
            CancellationToken token = default)
        {
            var dtos = await _mediator
                .Send<IEnumerable<GetListAccountDto>?>(
                    new GetListAccountQuery(
                        RequestInfo.CorrelationToken,
                        page,
                        amount),
                    token);

            var getListAccountDtos = dtos as GetListAccountDto[] 
                                     ?? dtos.ToArray();

            return !getListAccountDtos.Any()
                ? (ActionResult) NoContent()
                : Ok(
                    getListAccountDtos
                        .Select(
                            _ => new GetListAccountResponseVM
                            {
                                AccountStatus = _.AccountStatus,
                                Email = _.Email,
                                ProvinceId = _.ProvinceId
                            }));
        }

        [HttpPost]
        public async Task<ActionResult?> Register(
            [FromBody] AccountRegisterRequestVM viewModel,
            CancellationToken token = default)
        {
            var dto = await _mediator
                .Send<AccountRegisterDto?>(
                    new AccountRegisterCommand(
                        RequestInfo.CorrelationToken,
                        AccountStatusEnum.Active,
                        viewModel.Email,
                        viewModel.Password,
                        viewModel.ProvinceId,
                        viewModel.Agree),
                    token);

            if (dto == null)
                return BadRequest($"Account {viewModel.Email} already registered");

            return CreatedAtAction(
                nameof(GetByEmail),
                new
                {
                    dto.Email
                },
                new AccountRegisterResponseVM
                {
                    AccountStatus = dto
                        .AccountStatus,
                    Email = dto
                        .Email,
                    Address = new AccountRegisterAddressResponseVM
                    {
                        CountryId = dto
                            .Address
                            .CountryId,
                        CountryTitle = dto
                            .Address
                            .CountryTitle,
                        ProvinceId = dto
                            .Address
                            .ProvinceId,
                        ProvinceTitle = dto
                            .Address
                            .ProvinceTitle
                    }
                });
        }
    }
}
