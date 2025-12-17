namespace StartupOrchestrator.Abstractions.Synchronization
{
    public interface IBarrierRegistration
    {
        Action<IRegistryContext>? Callback { get; set; }
        bool Executed { get; set; }
        IReadOnlyCollection<RegistryKey> Requirements { get; }

        void Require(RegistryKey key);
    }
}
