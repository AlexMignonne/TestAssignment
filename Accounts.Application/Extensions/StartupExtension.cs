using Accounts.Application.UseCases.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Application.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services
                .AddTransient<RegisterAccountUseCase>()
                .AddTransient<GetListAccountUseCase>();

            return services;
        }
    }
}
