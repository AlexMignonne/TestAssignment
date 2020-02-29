using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Domain.AggregatesModel.Address
{
    public interface IAddressQueries
    {
        Task<AddressDomain?> GetByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token);

        Task<bool> IsExistByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token);
    }
}
