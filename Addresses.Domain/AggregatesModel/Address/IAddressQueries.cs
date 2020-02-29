using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.AggregatesModel.Province;

namespace Addresses.Domain.AggregatesModel.Address
{
    public interface IAddressQueries
    {
        Task<ProvinceDomain?> GetByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token);
    }
}
