using StartupOrchestrator.Abstractions.Activation;

namespace StartupOrchestrator.Abstractions.Components
{
    public interface IWithActivator
    {
        IStartupActivator GetActivator();
    }
}
