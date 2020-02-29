using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.App.Commands.AddCountry;
using Addresses.Api.App.Commands.UpdateTitleCountry;
using Addresses.Api.App.Queries;
using Addresses.Api.App.Queries.GetByIdCountry;
using Addresses.Api.App.Queries.GetListCountry;
using Addresses.SharedLibrary.ViewModels;
using CommonLibrary.RequestInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Addresses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController
        : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IMediator _mediator;

        public CountriesController(
            ILogger<CountriesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add(
            [FromBody] CountryViewModel country,
            CancellationToken token = default)
        {
            var dto = await _mediator
                .Send<AddCountryDto>(
                    new AddCountryCommand(
                        RequestInfo.CorrelationToken,
                        country.Title),
                    token);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    dto.Id
                },
                new CountryInfoViewModel
                {
                    Id = dto.Id,
                    Title = dto.Title
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(
            [FromRoute] int id,
            CancellationToken token = default)
        {
            var dto = await _mediator
                .Send<GetByIdCountryDto?>(
                    new GetByIdCountryQuery(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            return dto == null!
                ? (ActionResult) NotFound()
                : Ok(
                    new CountryInfoViewModel
                    {
                        Id = dto.Id,
                        Title = dto.Title
                    });
        }

        [HttpGet]
        public async Task<ActionResult> GetList(
            [FromQuery] int page = 1,
            [FromQuery] int amount = 10,
            CancellationToken token = default)
        {
            var dtos = await _mediator
                .Send<IEnumerable<GetListCountryDto>?>(
                    new GetListCountryQuery(
                        RequestInfo.CorrelationToken,
                        page,
                        amount),
                    token);

            var countryDomains = dtos
                .ToList();

            return !countryDomains.Any()
                ? (ActionResult) NoContent()
                : Ok(
                    countryDomains
                        .Select(
                            _ => new CountryInfoViewModel
                            {
                                Id = _.Id,
                                Title = _.Title
                            }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(
            [FromRoute] int id,
            CancellationToken token = default)
        {
            var result = await _mediator
                .Send<bool>(
                    new RemoveCountryCommand(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            return result
                ? (ActionResult) Ok()
                : NotFound();
        }

        [HttpPut("title")]
        public async Task<ActionResult> UpdateTitle(
            [FromBody] CountryInfoViewModel countryInfoViewModel,
            CancellationToken token = default)
        {
            var dto = await _mediator
                .Send<UpdateTitleCountryDto?>(
                    new UpdateTitleCountryCommand(
                        RequestInfo.CorrelationToken,
                        countryInfoViewModel.Id,
                        countryInfoViewModel.Title),
                    token);

            return dto == null!
                ? (ActionResult) NotFound()
                : Ok(
                    new CountryInfoViewModel
                    {
                        Id = dto.Id,
                        Title = dto.Title
                    });
        }
    }
}
