namespace StartupOrchestrator.Abstractions.Synchronization
{
    public interface IBarrierBuilder
    {
        IBarrierBuilder Register<T>(string? key = null)
            where T : notnull;

        public void RegisterCallback<T>(
            Action<IBarrierRegistryContext> callback, 
            string? key = null)
            where T : notnull;
    }
}
