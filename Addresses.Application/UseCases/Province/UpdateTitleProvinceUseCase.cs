using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Api.DataTransferObjects;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class UpdateTitleProvinceUseCase
        : IRequestHandler<
            UpdateTitleProvinceCommand,
            ProvinceDto>
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

        public async Task<ProvinceDto?> Handle(
            UpdateTitleProvinceCommand request,
            CancellationToken token)
        {
            if (!await _provinceQueries
                .IsExist(
                    request.CorrelationToken,
                    request.Id,
                    token))
                return null;

            var provinceDomain = await _provinceQueries
                .GetById(
                    request.CorrelationToken,
                    request.Id,
                    token);

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

            return new ProvinceDto(
                provinceDomain.Id,
                provinceDomain.Country.Id,
                provinceDomain.Title,
                null);
        }
    }
}
