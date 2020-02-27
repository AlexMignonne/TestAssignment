using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Domain.AggregatesModel.Province
{
    public interface IProvinceQueries
    {
        Task<ProvinceDomain> GetById(
            string correlationToken,
            int id,
            CancellationToken token);

        Task<bool> IsExist(
            string correlationToken,
            int id,
            CancellationToken token);
    }
}
