using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Domain.AggregatesModel.Account
{
    public interface IAccountQueries
    {
        Task<IEnumerable<AccountDomain>> GetList(
            string correlationToken,
            int page = 1,
            int amount = 10,
            CancellationToken token = default);

        Task<AccountDomain> GetById(
            string correlationToken,
            int id,
            CancellationToken token);

        Task<AccountDomain> GetByEmail(
            string correlationToken,
            string email,
            CancellationToken token);

        Task<IEnumerable<AccountDomain>> GetByProvinceIds(
            string correlationToken,
            IEnumerable<int> ids,
            CancellationToken token);

        Task<bool> IsExist(
            string correlationToken,
            int id,
            CancellationToken token);

        Task<bool> IsExistByEmail(
            string correlationToken,
            string email,
            CancellationToken token);
    }
}
