using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Address;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Address
{
    public sealed class GetByProvinceIdAddressUseCase
        : IRequestHandler<
            GetByProvinceIdAddressQuery,
            ProvinceDto>
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

        public async Task<ProvinceDto?> Handle(
            GetByProvinceIdAddressQuery request,
            CancellationToken token)
        {
            if (!await _provinceQueries
                .IsExist(
                    request.CorrelationToken,
                    request.ProvinceId,
                    token))
                return null;

            var provinceDomain = await _addressQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    request.ProvinceId,
                    token);

            return new ProvinceDto(
                provinceDomain.Id,
                provinceDomain.Country.Id,
                provinceDomain.Title,
                new CountryDto(
                    provinceDomain.Country.Id,
                    provinceDomain.Country.Title,
                    null));
        }
    }
}
