using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class AddCountryUseCase
        : IRequestHandler<
            AddCountryCommand,
            CountryDto>
    {
        private readonly ICountryCommands _countryCommands;
        private readonly ILogger<AddCountryUseCase> _logger;

        public AddCountryUseCase(
            ILogger<AddCountryUseCase> logger,
            ICountryCommands countryCommands)
        {
            _logger = logger;
            _countryCommands = countryCommands;
        }

        public async Task<CountryDto> Handle(
            AddCountryCommand request,
            CancellationToken token)
        {
            var countryDomain = await _countryCommands
                .Add(
                    request.CorrelationToken,
                    new CountryDomain(
                        request.CorrelationToken,
                        request.Title),
                    token);

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
