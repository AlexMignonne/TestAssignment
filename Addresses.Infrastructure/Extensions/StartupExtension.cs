using System;
using System.Linq;
using Addresses.Domain.AggregatesModel.Address;
using Addresses.Domain.AggregatesModel.Country;
using Addresses.Domain.AggregatesModel.Province;
using Addresses.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Addresses.Infrastructure.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("AddressesConnection");

            services
                .AddDbContext<AddressesContext>(
                    options =>
                        options.UseNpgsql(connectionString))
                .AddTransient<IAddressQueries, AddressRepository>()
                .AddTransient<ICountryCommands, CountryRepository>()
                .AddTransient<ICountryQueries, CountryRepository>()
                .AddTransient<IProvinceCommands, ProvinceRepository>()
                .AddTransient<IProvinceQueries, ProvinceRepository>();

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
                .GetService<AddressesContext>();

            var waitAndRetry = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    new[]
                    {
                        TimeSpan.FromSeconds(10),
                        TimeSpan.FromSeconds(20),
                        TimeSpan.FromSeconds(30)
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
