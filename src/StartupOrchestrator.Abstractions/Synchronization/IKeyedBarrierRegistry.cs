namespace StartupOrchestrator.Abstractions.Synchronization
{
    public interface IKeyedBarrierRegistry
    {
        public void AddDependency(
            IBarrierRegistration registration,
            RegistryKey key,
            Action<IRegistryContext>? callback);

        public IBarrierBuilder Register<T>(string? key = null)
            where T : notnull;

        public void RegisterCallback<T>(
            Action<IRegistryContext> callback,
            string? key = null)
            where T : notnull;

        public void Trigger<T>(T instance, string? key = null)
            where T : notnull;
    }
}
