using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.AggregatesModel.Province;
using Addresses.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Addresses.Infrastructure.Repositories
{
    public sealed class ProvinceRepository
        : IProvinceCommands,
            IProvinceQueries
    {
        private readonly AddressesContext _addressesContext;
        private readonly ILogger<ProvinceRepository> _logger;

        public ProvinceRepository(
            ILogger<ProvinceRepository> logger,
            AddressesContext addressesContext)
        {
            _logger = logger;
            _addressesContext = addressesContext;
        }

        public IUnitOfWork UnitOfWork => _addressesContext;

        public async Task<ProvinceDomain> Add(
            string correlationToken,
            ProvinceDomain province,
            CancellationToken token)
        {
            var entityEntry = await _addressesContext
                .Province
                .AddAsync(
                    province,
                    token);

            return entityEntry.Entity;
        }

        public void Update(
            string correlationToken,
            ProvinceDomain province)
        {
            _addressesContext
                .Province
                .Update(province);
        }

        public async Task<bool> Remove(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            var provinceDomain = await GetById(
                correlationToken,
                id,
                token);

            if (provinceDomain == null!)
                return false;

            _addressesContext
                .Province
                .Remove(provinceDomain);

            return true;
        }

        public async Task<ProvinceDomain> GetById(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _addressesContext
                .Province
                .FindAsync(
                    id);
        }

        public async Task<bool> IsExist(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _addressesContext
                .Province
                .AnyAsync(
                    _ => _.Id == id,
                    token);
        }
    }
}
