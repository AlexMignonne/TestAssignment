using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries.GetByProvinceIdAddress;
using Addresses.SharedLibrary.ViewModels;
using CommonLibrary.RequestInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Addresses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController
        : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IMediator _mediator;

        public AddressesController(
            ILogger<AddressesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("province/{id}")]
        public async Task<ActionResult> GetByProvinceId(
            int id,
            CancellationToken token)
        {
            var dto = await _mediator
                .Send<GetByProvinceIdAddressDto?>(
                    new GetByProvinceIdAddressQuery(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            if (dto == null)
                return NotFound();

            var addressViewModel = new AddressViewModel
            {
                CountryId = dto
                    .CountryId,
                CountryTitle = dto
                    .CountryTitle,
                ProvinceId = dto
                    .ProvinceId,
                ProvinceTitle = dto
                    .ProvinceTitle
            };

            return Ok(
                addressViewModel);
        }
    }
}
