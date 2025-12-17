namespace StartupOrchestrator.Abstractions.Synchronization
{
    public interface IBarrierRegistration
    {
        Action<IBarrierRegistryContext>? Callback { get; set; }
        bool Executed { get; set; }
        IReadOnlyCollection<BarrierRegistryKey> Requirements { get; }

        void Require(BarrierRegistryKey key);
    }
}
