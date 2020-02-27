using System.IO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Accounts.Infrastructure
{
    public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<AccountsContext>
    {
        public AccountsContext CreateDbContext(
            string[] args)
        {
            // TODO add env and isDevelopment check
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AccountsContext>();

            var connectionString = configuration
                .GetConnectionString("AccountsConnection");

            builder
                .UseNpgsql(connectionString);

            var factory = new ServiceFactory(
                _ => GetType());

            return new AccountsContext(
                builder.Options,
                new Mediator(factory));
        }
    }
}
