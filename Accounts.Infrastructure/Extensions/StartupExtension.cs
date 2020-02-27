using System;
using System.Linq;
using Accounts.Domain.AggregatesModel.Account;
using Accounts.Domain.AggregatesModel.Address;
using Accounts.Infrastructure.HttpClients;
using Accounts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Accounts.Infrastructure.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString(
                    "AccountsConnection");

            services
                .AddDbContext<AccountsContext>(
                    options =>
                        options.UseNpgsql(connectionString))
                .AddTransient<IAccountCommand, AccountRepository>()
                .AddTransient<IAccountQueries, AccountRepository>()
                .AddTransient<IAddressQueries, AddressRepository>()
                .AddHttpClient<AddressesHttpClient>();

            return services;
        }

        public static void ApplyMigration(
            this IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope
                .ServiceProvider
                .GetService<AccountsContext>();

            var waitAndRetry = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    new[]
                    {
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(7),
                        TimeSpan.FromSeconds(20)
                    });

            if (context.Database.GetPendingMigrations().Any())
                waitAndRetry
                    .Execute(
                        () =>
                        {
                            // ReSharper disable once AccessToDisposedClosure
                            context
                                .Database
                                .Migrate();
                        });
        }
    }
}
