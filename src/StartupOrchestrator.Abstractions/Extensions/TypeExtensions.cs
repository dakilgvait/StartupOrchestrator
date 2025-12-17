using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Abstractions.Extensions
{
    public static partial class StartupOrchestratorExtensions
    {
        public static BarrierRegistryKey CreateKey(this Type type, string? key)
        {
            return new BarrierRegistryKey()
            {
                Key = key,
                Type = type
            };
        }
    }
}
