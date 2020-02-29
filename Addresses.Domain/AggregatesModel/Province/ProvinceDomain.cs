using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.DomainEvents;
using Addresses.Domain.Exceptions;
using Addresses.Domain.SeedWork;

namespace Addresses.Domain.AggregatesModel.Province
{
    public sealed class ProvinceDomain
        : Entity,
            IAggregateRoot
    {
        public ProvinceDomain(
            string correlationId,
            string title)
        {
            Id = default;
            CountryId = default;
            Country = default!;

            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException(
                    $"{nameof(Title)} cannot be empty.")
                : title;

            AddDomainEvent(
                new AddProvinceDomainEvent(
                    correlationId,
                    this));
        }

        // ReSharper disable once UnusedMember.Local
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private ProvinceDomain()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public new int Id { get; private set; }
        public string Title { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public int CountryId { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public CountryDomain Country { get; private set; }

        public void UpdateTitle(
            string correlationId,
            string? title)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException(
                    $"{nameof(Title)} cannot be empty.")
                : title;
#pragma warning restore CS8601 // Possible null reference assignment.

            AddDomainEvent(
                new UpdateTitleProvinceDomainEvent(
                    correlationId,
                    this));
        }
    }
}
