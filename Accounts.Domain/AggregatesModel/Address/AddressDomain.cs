using System.Diagnostics.CodeAnalysis;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.Exceptions;

namespace Accounts.Domain.AggregatesModel.Address
{
    [SuppressMessage(
        "ReSharper",
        "AutoPropertyCanBeMadeGetOnly.Local")]
    public sealed class AddressDomain
    {
        public AddressDomain(
            int countryId,
            string countryTitle,
            int provinceId,
            string provinceTitle)
        {
            Id = default;
            Account = default!;

            CountryId = countryId == default
                ? throw new DomainException(
                    $"{nameof(CountryId)} cannot be default value.")
                : countryId;

            CountryTitle = string.IsNullOrWhiteSpace(countryTitle)
                ? throw new DomainException(
                    $"{nameof(CountryTitle)} cannot ne empty.")
                : countryTitle;

            ProvinceId = provinceId == default
                ? throw new DomainException(
                    $"{nameof(ProvinceId)} cannot be default value.")
                : provinceId;

            ProvinceTitle = string.IsNullOrWhiteSpace(provinceTitle)
                ? throw new DomainException(
                    $"{nameof(ProvinceTitle)} cannot ne empty.")
                : provinceTitle;
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private AddressDomain()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
        }

        public int Id { get; private set; }
        public AccountDomain Account { get; private set; }
        public int CountryId { get; private set; }
        public string CountryTitle { get; private set; }
        public int ProvinceId { get; private set; }
        public string ProvinceTitle { get; private set; }
    }
}
