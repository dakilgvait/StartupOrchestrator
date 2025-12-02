using Microsoft.Extensions.Hosting;

namespace StartupOrchestrator.Abstractions;

public interface IInitializerOrchestrator
{
    void Initialize(IHostApplicationBuilder builder);
}
