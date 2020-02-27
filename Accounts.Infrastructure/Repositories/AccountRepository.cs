using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accounts.Infrastructure.Repositories
{
    public sealed class AccountRepository
        : IAccountCommand,
            IAccountQueries
    {
        private readonly AccountsContext _accountsContext;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(
            ILogger<AccountRepository> logger,
            AccountsContext accountsContext)
        {
            _logger = logger;
            _accountsContext = accountsContext;
        }

        public IUnitOfWork UnitOfWork => _accountsContext;

        public async Task<AccountDomain> Register(
            string correlationToken,
            AccountDomain account,
            CancellationToken token)
        {
            var entityEntry = await _accountsContext
                .Accounts
                .AddAsync(
                    account,
                    token);

            return entityEntry.Entity;
        }

        public async Task<IEnumerable<AccountDomain>> GetList(
            string correlationToken,
            int page = 1,
            int amount = 10,
            CancellationToken token = default)
        {
            if (amount < 5)
                amount = 5;

            var offset = page < 1
                ? 1
                : (page - 1) * amount;

            var countryDomains = await _accountsContext
                .Accounts
                .Skip(offset)
                .Take(amount)
                .OrderBy(_ => _.Id)
                .ToListAsync(token);

            return countryDomains
                .AsReadOnly();
        }

        public async Task<AccountDomain> GetById(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _accountsContext
                .Accounts
                .FindAsync(
                    id);
        }

        public async Task<AccountDomain> GetByEmail(
            string correlationToken,
            string email,
            CancellationToken token)
        {
            return await _accountsContext
                .Accounts
                .SingleAsync(
                    _ => _.Email == email,
                    token);
        }

        public async Task<bool> IsExist(
            string correlationToken,
            int id,
            CancellationToken token)
        {
            return await _accountsContext
                .Accounts
                .AnyAsync(
                    _ => _.Id == id,
                    token);
        }

        public async Task<bool> IsExistByEmail(
            string correlationToken,
            string email,
            CancellationToken token)
        {
            return await _accountsContext
                .Accounts
                .AnyAsync(
                    _ => _.Email == email,
                    token);
        }
    }
}
