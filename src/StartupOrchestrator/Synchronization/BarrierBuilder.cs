using StartupOrchestrator.Abstractions.Extensions;
using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Synchronization
{
    public sealed class BarrierBuilder : IBarrierBuilder
    {
        private readonly IBarrierRegistration _registration;
        private readonly IKeyedBarrierRegistry _registry;

        public BarrierBuilder(
            IKeyedBarrierRegistry registry,
            IBarrierRegistration registration)
        {
            _registry = registry;
            _registration = registration;
        }

        public IBarrierBuilder Register<T>(string? key = null)
            where T : notnull
        {
            _registry.AddDependency(_registration, typeof(T).GetRegistryKey(key), callback: null);

            return this;
        }

        public void RegisterCallback<T>(Action<IRegistryContext> callback, string? key = null)
            where T : notnull
        {
            _registry.AddDependency(_registration, typeof(T).GetRegistryKey(key), callback);
        }
    }
}
