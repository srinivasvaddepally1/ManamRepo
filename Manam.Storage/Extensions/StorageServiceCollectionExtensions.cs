using Microsoft.Extensions.DependencyInjection;

namespace Manam.Storage.Extensions;

/// <summary>
/// Extension methods for registering repository/storage services
/// </summary>
public static class StorageServiceCollectionExtensions
{
    /// <summary>
    /// Adds repository/storage services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddStorageServices(this IServiceCollection services)
    {
        // Repositories to be implemented

        return services;
    }
}
