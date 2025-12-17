namespace StartupOrchestrator.Abstractions.Synchronization
{
    public interface IBarrierActionRegistry
    {
        public void AddDependency(
            IBarrierRegistration registration,
            BarrierRegistryKey key,
            Action<IBarrierRegistryContext>? callback);

        public IBarrierBuilder Register<T>(string? key = null)
            where T : notnull;

        public void RegisterCallback<T>(
            Action<IBarrierRegistryContext> callback,
            string? key = null)
            where T : notnull;

        public void Trigger<T>(T instance, string? key = null)
            where T : notnull;
    }
}
