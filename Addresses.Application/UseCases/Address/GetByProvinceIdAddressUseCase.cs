using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries.GetByProvinceIdAddress;
using Addresses.Domain.AggregatesModel.Address;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Address
{
    public sealed class GetByProvinceIdAddressUseCase
        : IRequestHandler<
            GetByProvinceIdAddressQuery,
            GetByProvinceIdAddressDto?>
    {
        private readonly IAddressQueries _addressQueries;
        private readonly ILogger<GetByProvinceIdAddressUseCase> _logger;
        private readonly IProvinceQueries _provinceQueries;

        public GetByProvinceIdAddressUseCase(
            ILogger<GetByProvinceIdAddressUseCase> logger,
            IAddressQueries addressQueries,
            IProvinceQueries provinceQueries)
        {
            _logger = logger;
            _addressQueries = addressQueries;
            _provinceQueries = provinceQueries;
        }

        public async Task<GetByProvinceIdAddressDto?> Handle(
            GetByProvinceIdAddressQuery request,
            CancellationToken token)
        {
            var provinceDomain = await _addressQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    request.ProvinceId,
                    token);

            return provinceDomain == null
                ? null
                : new GetByProvinceIdAddressDto(
                    provinceDomain.CountryId,
                    provinceDomain.Country.Title,
                    provinceDomain.Id,
                    provinceDomain.Title);
        }
    }
}
