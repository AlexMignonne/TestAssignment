using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Domain.AggregatesModel.Country
{
    public interface ICountryQueries
    {
        Task<IEnumerable<CountryDomain>> GetList(
            string correlationToken,
            int page = 1,
            int amount = 10,
            CancellationToken token = default);

        Task<CountryDomain?> GetById(
            string correlationToken,
            int id,
            CancellationToken token);

        Task<CountryDomain?> GetByProvinceId(
            string correlationToken,
            int id,
            CancellationToken token);

        Task<bool> IsExist(
            string correlationToken,
            int id,
            CancellationToken token);
    }
}
