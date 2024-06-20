using Ebanx.Interfaces;
using Ebanx.Services;

namespace Ebanx.Configurations;

public static class DependencyInjectionSetup
{
    public static void AddDependencyInjectionSetup(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
    }
}
