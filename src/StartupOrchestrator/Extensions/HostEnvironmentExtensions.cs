using Microsoft.Extensions.Hosting;

namespace StartupOrchestrator.Extensions;

public static partial class StartupOrchestratorExtensions
{
    public static readonly string Localhost = "Localhost";

    public static bool IsLocalhost(this IHostEnvironment environment)
    {
        return environment.IsEnvironment(Localhost);
    }
}
