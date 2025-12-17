using StartupOrchestrator.Abstractions.Orchestration;

namespace StartupOrchestrator.Abstractions.Components
{
    public interface IWithSorter
    {
        string Key { get; }

        IStartupSorter GetSorter();
    }
}
