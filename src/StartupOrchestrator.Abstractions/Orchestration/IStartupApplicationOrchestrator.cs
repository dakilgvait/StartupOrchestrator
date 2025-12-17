using Microsoft.AspNetCore.Builder;

namespace StartupOrchestrator.Abstractions.Orchestration
{
    public interface IStartupApplicationOrchestrator<out TApplication>
        where TApplication : IApplicationBuilder
    {
        void Run(Action<TApplication> action);
    }
}
