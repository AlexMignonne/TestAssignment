using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands.UpdateTitleCountry;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class UpdateTitleCountryUseCase
        : IRequestHandler<
            UpdateTitleCountryCommand,
            UpdateTitleCountryDto?>
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

        public async Task<UpdateTitleCountryDto?> Handle(
            UpdateTitleCountryCommand request,
            CancellationToken token)
        {
            var countryDomain = await _countryQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

            countryDomain
                ?.UpdateTitle(
                    request.CorrelationToken,
                    request.Title);

            if (countryDomain == null)
                return null;

            if (!await _countryCommands
                .Update(
                    request.CorrelationToken,
                    countryDomain,
                    token))
                return null;

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new UpdateTitleCountryDto(
                countryDomain.Id,
                countryDomain.Title);
        }
    }
}
