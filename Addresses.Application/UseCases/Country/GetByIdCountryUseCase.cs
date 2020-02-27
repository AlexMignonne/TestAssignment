using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class GetByIdCountryUseCase
        : IRequestHandler<
            GetByIdCountryQuery,
            CountryDto>
    {
        private readonly ICountryQueries _countryQueries;
        private readonly ILogger<GetByIdCountryUseCase> _logger;

        public GetByIdCountryUseCase(
            ILogger<GetByIdCountryUseCase> logger,
            ICountryQueries countryQueries)
        {
            _logger = logger;
            _countryQueries = countryQueries;
        }

        public async Task<CountryDto?> Handle(
            GetByIdCountryQuery request,
            CancellationToken token)
        {
            if (!await _countryQueries
                .IsExist(
                    request.CorrelationToken,
                    request.Id,
                    token))
                return null;

            var countryDomain = await _countryQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

            return new CountryDto(
                countryDomain.Id,
                countryDomain.Title,
                countryDomain.Provinces
                    .Select(
                        _ => new ProvinceDto(
                            _.Id,
                            countryDomain.Id,
                            _.Title,
                            null))
                    .ToList());
        }
    }
}
