using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.App.Commands.AddProvince;
using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class AddProvinceUseCase
        : IRequestHandler<
            AddProvinceCommand,
            AddProvinceDto?>
    {
        private readonly ICountryCommands _countryCommands;
        private readonly ICountryQueries _countryQueries;
        private readonly ILogger<AddProvinceUseCase> _logger;

        public AddProvinceUseCase(
            ILogger<AddProvinceUseCase> logger,
            ICountryCommands countryCommands,
            ICountryQueries countryQueries)
        {
            _logger = logger;
            _countryCommands = countryCommands;
            _countryQueries = countryQueries;
        }

        public async Task<AddProvinceDto?> Handle(
            AddProvinceCommand request,
            CancellationToken token)
        {
            var countryDomain = await _countryQueries
                .GetById(
                    request.CorrelationToken,
                    request.CountryId,
                    token);

            if (countryDomain == null)
                return null;

            var provinceDomain = new ProvinceDomain(
                request.CorrelationToken,
                request.Title);

            countryDomain
                .ProvinceAdd(provinceDomain);

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new AddProvinceDto(
                provinceDomain.Id,
                provinceDomain.Country.Id,
                provinceDomain.Title);
        }
    }
}
