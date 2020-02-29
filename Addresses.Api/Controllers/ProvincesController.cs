using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.App.Commands.AddProvince;
using Addresses.Api.App.Commands.UpdateTitleProvince;
using Addresses.Api.App.Queries;
using Addresses.Api.App.Queries.GetByIdProvince;
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
            var dto = await _mediator
                .Send<AddProvinceDto?>(
                    new AddProvinceCommand(
                        RequestInfo.CorrelationToken,
                        province.CountryId,
                        province.Title),
                    token);

            if (dto == null)
                return BadRequest(
                    $"Country with id {province.CountryId} not exist");

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    dto.Id
                },
                new ProvinceInfoViewModel
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
                .Send<GetByIdProvinceDto?>(
                    new GetByIdProvinceQuery(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            return dto == null!
                ? (ActionResult) NotFound()
                : Ok(
                    new ProvinceInfoViewModel
                    {
                        Id = dto.Id,
                        Title = dto.Title
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
            var dto = await _mediator
                .Send<UpdateTitleProvinceDto?>(
                    new UpdateTitleProvinceCommand(
                        RequestInfo.CorrelationToken,
                        provinceInfo.Id,
                        provinceInfo.Title),
                    token);

            return dto == null!
                ? (ActionResult) NotFound()
                : Ok(
                    new ProvinceInfoViewModel
                    {
                        Id = dto.Id,
                        Title = dto.Title
                    });
        }
    }
}
