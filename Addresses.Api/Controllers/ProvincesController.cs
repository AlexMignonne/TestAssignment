using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.App.Queries;
using Addresses.Api.DataTransferObjects;
using Addresses.SharedLibrary.ViewModels;
using CommonLibrary.RequestInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Addresses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvincesController
        : ControllerBase
    {
        private readonly ILogger<ProvincesController> _logger;
        private readonly IMediator _mediator;

        public ProvincesController(
            ILogger<ProvincesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add(
            [FromBody] ProvinceViewModel province,
            CancellationToken token = default)
        {
            var provinceDomain = await _mediator
                .Send<ProvinceDto>(
                    new AddProvinceCommand(
                        RequestInfo.CorrelationToken,
                        province.CountryId,
                        province.Title),
                    token);

            if (provinceDomain == null)
                return BadRequest(
                    $"Country with id {province.CountryId} not exist");

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    provinceDomain.Id
                },
                new ProvinceInfoViewModel
                {
                    Id = provinceDomain.Id,
                    Title = provinceDomain.Title
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(
            [FromRoute] int id,
            CancellationToken token = default)
        {
            var provinceDomain = await _mediator
                .Send<ProvinceDto>(
                    new GetByIdProvinceQuery(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            return provinceDomain == null!
                ? (ActionResult) NotFound()
                : Ok(
                    new ProvinceInfoViewModel
                    {
                        Id = provinceDomain.Id,
                        Title = provinceDomain.Title
                    });
        }

        [HttpGet("is_exist/{id}")]
        public async Task<IActionResult> IsExistProvinceById(
            int id)
        {
            return await _mediator
                .Send<bool>(
                    new IsExistProvinceByIdQuery(
                        RequestInfo.CorrelationToken,
                        id))
                ? (IActionResult) Ok()
                : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(
            [FromRoute] int id,
            CancellationToken token = default)
        {
            var result = await _mediator
                .Send<bool>(
                    new RemoveProvinceCommand(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            return result
                ? (ActionResult) Ok()
                : NotFound();
        }

        [HttpPut("title")]
        public async Task<ActionResult> UpdateTitle(
            [FromBody] ProvinceInfoViewModel provinceInfo,
            CancellationToken token = default)
        {
            var countryDomain = await _mediator
                .Send<ProvinceDto>(
                    new UpdateTitleProvinceCommand(
                        RequestInfo.CorrelationToken,
                        provinceInfo.Id,
                        provinceInfo.Title),
                    token);

            return countryDomain == null!
                ? (ActionResult) NotFound()
                : Ok(
                    new ProvinceInfoViewModel
                    {
                        Id = countryDomain.Id,
                        Title = countryDomain.Title
                    });
        }
    }
}
