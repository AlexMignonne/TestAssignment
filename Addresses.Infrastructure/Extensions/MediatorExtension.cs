using System.Linq;
using System.Threading.Tasks;
using Addresses.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Addresses.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync<TContext>(
            this IMediator mediator,
            TContext ctx)
            where TContext : DbContext
        {
            var domainEntities = ctx
                .ChangeTracker
                .Entries<Entity>()
                .Where(
                    _ =>
                        _.Entity.DomainEvents != null && _.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(_ => _.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ToList()
                .ForEach(
                    _ => _
                        .Entity
                        .ClearDomainEvents());

            var tasks = domainEvents
                .Select(async _ => await mediator.Publish(_));

            await Task.WhenAll(tasks);
        }
    }
}
