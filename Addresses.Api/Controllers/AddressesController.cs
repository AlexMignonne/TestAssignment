using System.Threading;
using System.Threading.Tasks;
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
            var provinceDomain = await _mediator
                .Send<ProvinceDto>(
                    new GetByProvinceIdAddressQuery(
                        RequestInfo.CorrelationToken,
                        id),
                    token);

            if (provinceDomain == null!)
                return NotFound();

            var addressViewModel = new AddressViewModel
            {
                CountryId = provinceDomain
                    .Country
                    .Id,
                CountryTitle = provinceDomain
                    .Country
                    .Title,
                ProvinceId = provinceDomain
                    .Id,
                ProvinceTitle = provinceDomain
                    .Title
            };

            return Ok(
                addressViewModel);
        }
    }
}
