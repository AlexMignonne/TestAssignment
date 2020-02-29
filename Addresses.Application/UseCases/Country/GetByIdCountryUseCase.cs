using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries.GetByIdCountry;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class GetByIdCountryUseCase
        : IRequestHandler<
            GetByIdCountryQuery,
            GetByIdCountryDto?>
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

        public async Task<GetByIdCountryDto?> Handle(
            GetByIdCountryQuery request,
            CancellationToken token)
        {
            var countryDomain = await _countryQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

            return countryDomain == null
                ? null
                : new GetByIdCountryDto(
                    countryDomain.Id,
                    countryDomain.Title,
                    countryDomain.Provinces
                        .Select(
                            _ => new GetByIdCountryProvinceDto(
                                _.Id,
                                _.Title)));
        }
    }
}
