using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.Account
{
    public interface IAccountCommand
        : IRepository<AccountDomain>
    {
        Task<AccountDomain> Register(
            string correlationToken,
            AccountDomain account,
            CancellationToken token);
    }
}
