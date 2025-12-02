using Microsoft.Extensions.Hosting;

namespace StartupOrchestrator.Extensions;

public static class HostEnvironmentExtensions
{
    public static readonly string Localhost = "Localhost";

    public static bool IsLocalhost(this IHostEnvironment environment)
    {
        return environment.IsEnvironment(Localhost);
    }
}
