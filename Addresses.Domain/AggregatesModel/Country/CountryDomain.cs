using System.Collections.Generic;
using System.Linq;
using Addresses.Domain.AggregatesModel.Province;
using Addresses.Domain.DomainEvents;
using Addresses.Domain.Exceptions;
using Addresses.Domain.SeedWork;

namespace Addresses.Domain.AggregatesModel.Country
{
    public sealed class CountryDomain
        : Entity,
            IAggregateRoot
    {
        private readonly List<ProvinceDomain> _provinces =
            new List<ProvinceDomain>();

        public CountryDomain(
            string correlationId,
            string title)
        {
            Id = default;

            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException(
                    $"{nameof(Title)} cannot be empty.")
                : title;

            AddDomainEvent(
                new AddCountryDomainEvent(
                    correlationId,
                    this));
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        // ReSharper disable once UnusedMember.Local
        private CountryDomain()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
        }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public new int Id { get; private set; }
        public string Title { get; private set; }

        public IEnumerable<ProvinceDomain> Provinces =>
            _provinces.AsReadOnly();

        public void UpdateTitle(
            string correlationId,
            string title)
        {
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException(
                    $"{nameof(Title)} cannot be empty.")
                : title;

            AddDomainEvent(
                new UpdateTitleCountryDomainEvent(
                    correlationId,
                    this));
        }

        public void ProvinceAdd(
            ProvinceDomain province)
        {
            _provinces.Add(province);
        }

        public bool ProvinceRemove(
            string correlationId,
            ProvinceDomain province)
        {
            if (!_provinces
                .Remove(province))
                return false;

            AddDomainEvent(
                new RemoveProvinceDomainEvent(
                    correlationId,
                    province));

            return true;
        }

        public bool ProvinceRemove(
            string correlationId,
            int provinceId)
        {
            var provinceDomain = _provinces
                .Single(
                    _ => _.Id == provinceId);

            if (provinceDomain == null)
                return false;

            _provinces
                .Remove(
                    provinceDomain);

            AddDomainEvent(
                new RemoveProvinceDomainEvent(
                    correlationId,
                    provinceDomain));

            return true;
        }
    }
}
