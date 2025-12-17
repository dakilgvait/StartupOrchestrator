using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Synchronization
{
    internal sealed class BarrierRegistration : IBarrierRegistration
    {
        private readonly HashSet<BarrierRegistryKey> _requirements = new();
        public Action<IBarrierRegistryContext>? Callback { get; set; }
        public bool Executed { get; set; }

        public IReadOnlyCollection<BarrierRegistryKey> Requirements
        {
            get { return _requirements; }
        }

        public void Require(BarrierRegistryKey key)
        {
            _requirements.Add(key);
        }
    }
}
