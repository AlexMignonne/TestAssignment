using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class UpdateTitleCountryUseCase
        : IRequestHandler<
            UpdateTitleCountryCommand,
            CountryDto>
    {
        private readonly ICountryCommands _countryCommands;
        private readonly ICountryQueries _countryQueries;
        private readonly ILogger<UpdateTitleCountryUseCase> _logger;

        public UpdateTitleCountryUseCase(
            ILogger<UpdateTitleCountryUseCase> logger,
            ICountryCommands countryCommands,
            ICountryQueries countryQueries)
        {
            _logger = logger;
            _countryCommands = countryCommands;
            _countryQueries = countryQueries;
        }

        public async Task<CountryDto?> Handle(
            UpdateTitleCountryCommand request,
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
                    false,
                    token);

            countryDomain
                .UpdateTitle(
                    request.CorrelationToken,
                    request.Title);

            _countryCommands
                .Update(
                    request.CorrelationToken,
                    countryDomain);

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new CountryDto(
                countryDomain.Id,
                countryDomain.Title,
                null);
        }
    }
}
