using System.IO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Addresses.Infrastructure
{
    public sealed class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<AddressesContext>
    {
        public AddressesContext CreateDbContext(
            string[] args)
        {
            // TODO add env and isDevelopment check
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AddressesContext>();

            var connectionString = configuration
                .GetConnectionString("AddressesConnection");

            builder
                .UseNpgsql(connectionString);

            var factory = new ServiceFactory(
                _ => GetType());

            return new AddressesContext(
                builder.Options,
                new Mediator(factory));
        }
    }
}
