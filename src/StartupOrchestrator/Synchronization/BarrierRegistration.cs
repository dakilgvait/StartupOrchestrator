using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Synchronization
{
    internal sealed class BarrierRegistration : IBarrierRegistration
    {
        private readonly HashSet<RegistryKey> _requirements = new();
        public Action<IRegistryContext>? Callback { get; set; }
        public bool Executed { get; set; }

        public IReadOnlyCollection<RegistryKey> Requirements
        {
            get { return _requirements; }
        }

        public void Require(RegistryKey key)
        {
            _requirements.Add(key);
        }
    }
}
