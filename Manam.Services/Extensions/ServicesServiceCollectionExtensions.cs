using Microsoft.Extensions.DependencyInjection;

namespace Manam.Services.Extensions;

public static class ServicesServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Services to be registered here
        return services;
    }
}
