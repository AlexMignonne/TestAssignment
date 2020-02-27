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
            Country = default;

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
        private ProvinceDomain()
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
            string title)
        {
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException(
                    $"{nameof(Title)} cannot be empty.")
                : title;

            AddDomainEvent(
                new UpdateTitleProvinceDomainEvent(
                    correlationId,
                    this));
        }
    }
}
