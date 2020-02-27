using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.AggregatesModel.Province;
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
        private readonly IProvinceQueries _provinceQueries;

        public RemoveProvinceUseCase(
            ILogger<RemoveProvinceUseCase> logger,
            ICountryCommands countryCommands,
            ICountryQueries countryQueries,
            IProvinceQueries provinceQueries)
        {
            _logger = logger;
            _countryCommands = countryCommands;
            _countryQueries = countryQueries;
            _provinceQueries = provinceQueries;
        }

        public async Task<bool> Handle(
            RemoveProvinceCommand request,
            CancellationToken token)
        {
            if (!await _provinceQueries
                .IsExist(
                    request.CorrelationToken,
                    request.Id,
                    token))
                return false;

            var countryDomain = await _countryQueries
                .GetByProvinceId(
                    request.CorrelationToken,
                    request.Id,
                    token);

            countryDomain
                .ProvinceRemove(
                    request.CorrelationToken,
                    request.Id);

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return true;
        }
    }
}
