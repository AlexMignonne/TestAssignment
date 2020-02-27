using Addresses.Domain.AggregatesModel.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Addresses.Infrastructure.EntityConfigurations
{
    public sealed class CountryTypeConfig
        : IEntityTypeConfiguration<CountryDomain>
    {
        public void Configure(
            EntityTypeBuilder<CountryDomain> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .Metadata
                .FindNavigation(nameof(CountryDomain.Provinces))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .Ignore(_ => _.DomainEvents);
        }
    }
}
