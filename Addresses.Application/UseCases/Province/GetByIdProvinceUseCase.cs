using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class GetByIdProvinceUseCase
        : IRequestHandler<
            GetByIdProvinceQuery,
            ProvinceDto>
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

        public async Task<ProvinceDto?> Handle(
            GetByIdProvinceQuery request,
            CancellationToken token)
        {
            if (!await _provinceQueries
                .IsExist(
                    request.CorrelationToken,
                    request.Id,
                    token))
                return null;

            var provinceDomain = await _provinceQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

            return new ProvinceDto(
                provinceDomain.Id,
                provinceDomain.CountryId,
                provinceDomain.Title,
                null);
        }
    }
}
