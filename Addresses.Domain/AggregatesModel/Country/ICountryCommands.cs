using System.Threading;
using System.Threading.Tasks;
using Addresses.Domain.SeedWork;

namespace Addresses.Domain.AggregatesModel.Country
{
    public interface ICountryCommands
        : IRepository<CountryDomain>
    {
        Task<CountryDomain?> Add(
            string correlationToken,
            CountryDomain country,
            CancellationToken token);

        Task<bool> Update(
            string correlationToken,
            CountryDomain country,
            CancellationToken token);

        Task<CountryDomain?> Remove(
            string correlationToken,
            int id,
            CancellationToken token);
    }
}
