using Microsoft.Extensions.DependencyInjection;

namespace Manam.Services.Extensions;

public static class UserManagementServiceExtensions
{
    public static IServiceCollection AddUserManagementServices(this IServiceCollection services)
    {
        // User management services to be registered here
        return services;
    }
}
