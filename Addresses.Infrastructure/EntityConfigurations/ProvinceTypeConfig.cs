using Addresses.Domain.AggregatesModel.Province;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Addresses.Infrastructure.EntityConfigurations
{
    public sealed class ProvinceTypeConfig
        : IEntityTypeConfiguration<ProvinceDomain>
    {
        public void Configure(
            EntityTypeBuilder<ProvinceDomain> builder)
        {
            builder
                .HasKey(_ => _.Id);

            builder
                .HasOne(_ => _.Country)
                .WithMany(_ => _.Provinces);

            builder
                .Ignore(_ => _.DomainEvents);
        }
    }
}
