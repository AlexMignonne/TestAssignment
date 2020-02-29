using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Queries;
using Addresses.Domain.AggregatesModel.Province;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Province
{
    public sealed class IsExistProvinceByIdUseCase
        : IRequestHandler<
            IsExistProvinceByIdQuery,
            bool>
    {
        private readonly ILogger<IsExistProvinceByIdUseCase> _logger;
        private readonly IProvinceQueries _provinceQueries;

        public IsExistProvinceByIdUseCase(
            ILogger<IsExistProvinceByIdUseCase> logger,
            IProvinceQueries provinceQueries)
        {
            _logger = logger;
            _provinceQueries = provinceQueries;
        }

        public Task<bool> Handle(
            IsExistProvinceByIdQuery request,
            CancellationToken token)
        {
            return _provinceQueries
                .IsExist(
                    request.CorrelationToken,
                    request.Id,
                    token);
        }
    }
}
