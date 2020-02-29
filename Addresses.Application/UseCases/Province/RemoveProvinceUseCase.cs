using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Domain.AggregatesModel.Country;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class RemoveProvinceUseCase
        : IRequestHandler<
            RemoveProvinceCommand,
            bool>
    {
        private readonly ICountryCommands _countryCommands;
        private readonly ICountryQueries _countryQueries;
        private readonly ILogger<RemoveProvinceUseCase> _logger;

        public RemoveProvinceUseCase(
            ILogger<RemoveProvinceUseCase> logger,
            ICountryCommands countryCommands,
            ICountryQueries countryQueries)
        {
            _logger = logger;
            _countryCommands = countryCommands;
            _countryQueries = countryQueries;
        }

        public async Task<bool> Handle(
            RemoveProvinceCommand request,
            CancellationToken token)
        {
            var countryDomain = await _countryQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    request.Id,
                    token);

            if (countryDomain == null)
                return false;

            if (!countryDomain
                .ProvinceRemove(
                    request.CorrelationToken,
                    request.Id))
                return false;

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return true;
        }
    }
}
