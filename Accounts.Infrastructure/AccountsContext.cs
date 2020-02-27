using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.SeedWork;
using Accounts.Infrastructure.EntityConfigurations;
using Accounts.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Accounts.Infrastructure
{
    public sealed class AccountsContext
        : DbContext,
            IUnitOfWork
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public AccountsContext(
            DbContextOptions options,
            IMediator mediator)
            : base(options)
        {
            _mediator = mediator ??
                        throw new ArgumentNullException(
                            nameof(mediator));
        }

        // ReSharper disable once UnusedMember.Local
        private AccountsContext()
        {
        }

        public DbSet<AccountDomain> Accounts { get; set; }

        public async Task<bool> SaveEntitiesAsync(
            CancellationToken token = default)
        {
            await _mediator
                .DispatchDomainEventsAsync(this);

            var result = await SaveChangesAsync(token);

            await _mediator
                .DispatchDomainEventsAsync(this);

            return result != 0;
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(
                    new AccountTypeConfig());
        }

        #region Transaction

        public IDbContextTransaction? GetCurrentTransaction()
        {
            return _currentTransaction;
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<IDbContextTransaction?> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return null;

            _currentTransaction = await Database
                .BeginTransactionAsync(
                    IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(
            IDbContextTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(
                    nameof(transaction));

            if (transaction != _currentTransaction)
                throw new InvalidOperationException(
                    $"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();

                transaction
                    .Commit();
            }
            catch
            {
                RollbackTransaction();

                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }

        #endregion
    }
}
