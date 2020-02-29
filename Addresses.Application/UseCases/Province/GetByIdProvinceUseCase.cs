using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries.GetByIdProvince;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class GetByIdProvinceUseCase
        : IRequestHandler<
            GetByIdProvinceQuery,
            GetByIdProvinceDto?>
    {
        private readonly ILogger<GetByIdProvinceUseCase> _logger;
        private readonly IProvinceQueries _provinceQueries;

        public GetByIdProvinceUseCase(
            ILogger<GetByIdProvinceUseCase> logger,
            IProvinceQueries provinceQueries)
        {
            _logger = logger;
            _provinceQueries = provinceQueries;
        }

        public async Task<GetByIdProvinceDto?> Handle(
            GetByIdProvinceQuery request,
            CancellationToken token)
        {
            var provinceDomain = await _provinceQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

            return provinceDomain == null
                ? null
                : new GetByIdProvinceDto(
                    provinceDomain.Id,
                    provinceDomain.CountryId,
                    provinceDomain.Title);
        }
    }
}
