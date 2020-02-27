using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.Address;
using Accounts.Infrastructure.HttpClients;
using Microsoft.Extensions.Logging;

namespace Accounts.Infrastructure.Repositories
{
    public sealed class AddressRepository
        : IAddressQueries
    {
        private readonly AddressesHttpClient _addressesHttpClient;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(
            ILogger<AddressRepository> logger,
            AddressesHttpClient addressesHttpClient)
        {
            _logger = logger;
            _addressesHttpClient = addressesHttpClient;
        }

        public async Task<AddressDomain> GetByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _addressesHttpClient
                .GetByProvinceId(
                    correlationToken,
                    id,
                    token);
        }

        public async Task<bool> IsExistByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _addressesHttpClient
                .IsExistProvinceById(
                    correlationToken,
                    id,
                    token);
        }
    }
}
