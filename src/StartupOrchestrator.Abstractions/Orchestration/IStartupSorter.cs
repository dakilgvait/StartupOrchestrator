namespace StartupOrchestrator.Abstractions.Orchestration
{
    public interface IStartupSorter
    {
        int GetOrder(Type type);
    }
}
