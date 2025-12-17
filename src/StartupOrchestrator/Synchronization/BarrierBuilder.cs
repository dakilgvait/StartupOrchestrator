using StartupOrchestrator.Abstractions.Extensions;
using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Synchronization
{
    public sealed class BarrierBuilder : IBarrierBuilder
    {
        private readonly IBarrierRegistration _registration;
        private readonly IBarrierActionRegistry _registry;

        public BarrierBuilder(
            IBarrierActionRegistry registry,
            IBarrierRegistration registration)
        {
            _registry = registry;
            _registration = registration;
        }

        public IBarrierBuilder Register<T>(string? key = null)
            where T : notnull
        {
            _registry.AddDependency(_registration, typeof(T).CreateKey(key), callback: null);

            return this;
        }

        public void RegisterCallback<T>(Action<IBarrierRegistryContext> callback, string? key = null)
            where T : notnull
        {
            _registry.AddDependency(_registration, typeof(T).CreateKey(key), callback);
        }
    }
}
