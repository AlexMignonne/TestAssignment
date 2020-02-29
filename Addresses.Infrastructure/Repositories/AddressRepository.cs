using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.AggregatesModel.Address;
using Addresses.Domain.AggregatesModel.Province;
using Microsoft.Extensions.Logging;

namespace Addresses.Infrastructure.Repositories
{
    public sealed class AddressRepository
        : IAddressQueries
    {
        private readonly AddressesContext _addressesContext;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(
            ILogger<AddressRepository> logger,
            AddressesContext addressesContext)
        {
            _logger = logger;
            _addressesContext = addressesContext;
        }

        public async Task<ProvinceDomain?> GetByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            var provinceDomain = await _addressesContext
                .Province
                .FindAsync(
                    id);

            if (provinceDomain == null)
                return null;

            await _addressesContext
                .Entry(provinceDomain)
                .Reference(_ => _.Country)
                .LoadAsync(token);

            return provinceDomain;
        }
    }
}
