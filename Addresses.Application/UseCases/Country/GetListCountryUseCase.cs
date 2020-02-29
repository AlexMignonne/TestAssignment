using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries.GetListCountry;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class GetListCountryUseCase
        : IRequestHandler<
            GetListCountryQuery,
            IEnumerable<GetListCountryDto>?>
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

        public async Task<IEnumerable<GetListCountryDto>?> Handle(
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
                    _ => new GetListCountryDto(
                        _.Id,
                        _.Title));
        }
    }
}
