using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.SeedWork;

namespace Addresses.Domain.AggregatesModel.Province
{
    public interface IProvinceCommands
        : IRepository<ProvinceDomain>
    {
        Task<ProvinceDomain> Add(
            string correlationToken,
            ProvinceDomain province,
            CancellationToken token);

        void Update(
            string correlationToken,
            ProvinceDomain province);

        Task<bool> Remove(
            string correlationToken,
            int id,
            CancellationToken token);
    }
}
