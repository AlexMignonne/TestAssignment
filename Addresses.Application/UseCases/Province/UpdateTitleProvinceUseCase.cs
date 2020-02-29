using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands.UpdateTitleProvince;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class UpdateTitleProvinceUseCase
        : IRequestHandler<
            UpdateTitleProvinceCommand,
            UpdateTitleProvinceDto?>
    {
        private readonly ILogger<UpdateTitleProvinceUseCase> _logger;
        private readonly IProvinceCommands _provinceCommands;
        private readonly IProvinceQueries _provinceQueries;

        public UpdateTitleProvinceUseCase(
            ILogger<UpdateTitleProvinceUseCase> logger,
            IProvinceCommands provinceCommands,
            IProvinceQueries provinceQueries)
        {
            _logger = logger;
            _provinceCommands = provinceCommands;
            _provinceQueries = provinceQueries;
        }

        public async Task<UpdateTitleProvinceDto?> Handle(
            UpdateTitleProvinceCommand request,
            CancellationToken token)
        {
            var provinceDomain = await _provinceQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

            if (provinceDomain == null)
                return null;

            provinceDomain
                .UpdateTitle(
                    request.CorrelationToken,
                    request.Title);

            _provinceCommands
                .Update(
                    request.CorrelationToken,
                    provinceDomain);

            await _provinceCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            return new UpdateTitleProvinceDto(
                provinceDomain.Id,
                provinceDomain.CountryId,
                provinceDomain.Title);
        }
    }
}
