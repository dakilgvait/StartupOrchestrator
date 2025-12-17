using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Abstractions.Components
{
    public interface IWithRegistry
    {
        IKeyedBarrierRegistry Registry { get; }
    }
}
