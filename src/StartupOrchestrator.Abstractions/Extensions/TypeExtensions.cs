using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Abstractions.Extensions
{
    public static partial class StartupOrchestratorExtensions
    {
        public static RegistryKey GetRegistryKey(this Type type, string? key)
        {
            return new RegistryKey()
            {
                Key = key,
                Type = type
            };
        }
    }
}
