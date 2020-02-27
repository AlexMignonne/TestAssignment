using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.SeedWork;
using Accounts.Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.EntityConfigurations
{
    public sealed class AccountTypeConfig
        : IEntityTypeConfiguration<AccountDomain>
    {
        public void Configure(
            EntityTypeBuilder<AccountDomain> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Property(
                    _ => _.AccountStatus)
                .HasConversion(
                    type => type.Id,
                    key => Enumeration
                        .FromValue<AccountStatusType>(key));

            builder
                .Ignore(_ => _.DomainEvents);
        }
    }
}
