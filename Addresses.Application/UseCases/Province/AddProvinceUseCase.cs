using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class AddProvinceUseCase
        : IRequestHandler<
            AddProvinceCommand,
            ProvinceDto>
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

        public async Task<ProvinceDto?> Handle(
            AddProvinceCommand request,
            CancellationToken token)
        {
            if (!await _countryQueries
                .IsExist(
                    request.CorrelationToken,
                    request.CountryId,
                    token))
                return null;

            var countryDomain = await _countryQueries
                .GetById(
                    request.CorrelationToken,
                    request.CountryId,
                    false,
                    token);

            var provinceDomain = new ProvinceDomain(
                request.CorrelationToken,
                request.Title);

            countryDomain
                .ProvinceAdd(provinceDomain);

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new ProvinceDto(
                provinceDomain.Id,
                provinceDomain.Country.Id,
                provinceDomain.Title,
                null);
        }
    }
}
