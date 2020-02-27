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

        // ReSharper disable once UnusedMember.Local
        private CountryDomain()
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

        public void ProvinceRemove(
            string correlationId,
            ProvinceDomain province)
        {
            _provinces
                .Remove(province);

            AddDomainEvent(
                new RemoveProvinceDomainEvent(
                    correlationId,
                    province));
        }

        public void ProvinceRemove(
            string correlationId,
            int provinceId)
        {
            var provinceDomain = _provinces
                .Single(
                    _ => _.Id == provinceId);

            _provinces
                .Remove(
                    provinceDomain);

            AddDomainEvent(
                new RemoveProvinceDomainEvent(
                    correlationId,
                    provinceDomain));
        }
    }
}
