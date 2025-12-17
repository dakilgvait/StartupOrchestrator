namespace StartupOrchestrator.Abstractions.Synchronization
{
    public interface IBarrierRegistryContext
    {
        public T Get<T>()
            where T : notnull;

        public T Get<T>(string? key)
           where T : notnull;

        public bool TryGet<T>(out T? value)
           where T : notnull;

        public bool TryGet<T>(string? key, out T? value)
          where T : notnull;
    }
}
