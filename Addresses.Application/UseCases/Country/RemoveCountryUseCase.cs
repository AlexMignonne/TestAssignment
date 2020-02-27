using System.Threading;
using System.Threading.Tasks;
using Addresses.Api.App.Commands;
using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Addresses.Application.UseCases.Country
{
    public sealed class RemoveCountryUseCase
        : IRequestHandler<
            RemoveCountryCommand,
            bool>
    {
        private readonly ICountryCommands _countryCommands;
        private readonly ICountryQueries _countryQueries;
        private readonly ILogger<RemoveCountryUseCase> _logger;
        private readonly IMediator _mediator;

        public RemoveCountryUseCase(
            ILogger<RemoveCountryUseCase> logger,
            IMediator mediator,
            ICountryCommands countryCommands,
            ICountryQueries countryQueries)
        {
            _logger = logger;
            _mediator = mediator;
            _countryCommands = countryCommands;
            _countryQueries = countryQueries;
        }

        public async Task<bool> Handle(
            RemoveCountryCommand request,
            CancellationToken token)
        {
            if (!await _countryQueries
                .IsExist(
                    request.CorrelationToken,
                    request.Id,
                    token))
                return false;

            var countryDomain = await _countryCommands
                .Remove(
                    request.CorrelationToken,
                    request.Id,
                    token);

            if (countryDomain == null)
                return false;

            await _countryCommands
                .UnitOfWork
                .SaveEntitiesAsync(token);

            await _mediator.Publish(
                new RemoveCountryDomainEvent(
                    request.CorrelationToken,
                    countryDomain),
                token);

            return true;
        }
    }
}
