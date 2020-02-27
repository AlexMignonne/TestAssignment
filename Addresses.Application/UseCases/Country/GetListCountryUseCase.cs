using System.Collections.Generic;
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
    public sealed class GetListCountryUseCase
        : IRequestHandler<
            GetListCountryQuery,
            IEnumerable<CountryDto>>
    {
        private readonly ICountryQueries _countryQueries;
        private readonly ILogger<GetListCountryUseCase> _logger;

        public GetListCountryUseCase(
            ILogger<GetListCountryUseCase> logger,
            ICountryQueries countryQueries)
        {
            _logger = logger;
            _countryQueries = countryQueries;
        }

        public async Task<IEnumerable<CountryDto>> Handle(
            GetListCountryQuery request,
            CancellationToken token)
        {
            var countryDomains = await _countryQueries
                .GetList(
                    request.CorrelationToken,
                    request.Page,
                    request.Amount,
                    token);

            return countryDomains
                .Select(
                    _ => new CountryDto(
                        _.Id,
                        _.Title,
                        null));
        }
    }
}
