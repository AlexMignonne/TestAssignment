using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Addresses.Infrastructure.Repositories
{
    public sealed class CountryRepository
        : ICountryCommands,
            ICountryQueries
    {
        private readonly AddressesContext _addressesContext;
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(
            ILogger<CountryRepository> logger,
            AddressesContext addressesContext)
        {
            _logger = logger;
            _addressesContext = addressesContext;
        }

        public IUnitOfWork UnitOfWork => _addressesContext;

        public async Task<CountryDomain?> Add(
            string correlationToken,
            CountryDomain country,
            CancellationToken token)
        {
            var entityEntry = await _addressesContext
                .Countries
                .AddAsync(
                    country,
                    token);

            return entityEntry.Entity;
        }

        public async Task<bool> Update(
            string correlationToken,
            CountryDomain country,
            CancellationToken token)
        {
            if (!await IsExist(
                correlationToken,
                country.Id,
                token))
                return false;

            _addressesContext
                .Countries
                .Update(country);

            return true;
        }

        public async Task<CountryDomain?> Remove(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            var countryDomain = await _addressesContext
                .Countries
                .FindAsync(id);

            if (countryDomain == null)
                return null;

            _addressesContext
                .Countries
                .Remove(countryDomain);

            return countryDomain;
        }

        public async Task<IEnumerable<CountryDomain>> GetList(
            string correlationToken,
            int page = 1,
            int amount = 10,
            CancellationToken token = default)
        {
            if (amount < 5)
                amount = 5;

            var offset = page < 1
                ? 1
                : (page - 1) * amount;

            var countryDomains = await _addressesContext
                .Countries
                .Skip(offset)
                .Take(amount)
                .OrderBy(_ => _.Title)
                .ToListAsync(token);

            return countryDomains
                .AsReadOnly();
        }

        public async Task<CountryDomain?> GetById(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _addressesContext
                .Countries
                .Include(_ => _.Provinces)
                .SingleOrDefaultAsync(
                    _ => _.Id == id,
                    token);
        }

        public async Task<CountryDomain?> GetByProvinceId(
            string correlationToken,
            int provinceId,
            CancellationToken token)
        {
            return await _addressesContext
                .Countries
                .Include(_ => _.Provinces)
                .SingleOrDefaultAsync(
                    _ => _.Provinces
                        .Any(province => province.Id == provinceId),
                    token);
        }

        public async Task<bool> IsExist(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _addressesContext
                .Countries
                .AnyAsync(
                    _ => _.Id == id,
                    token);
        }
    }
}
