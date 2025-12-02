using Microsoft.Extensions.Hosting;

namespace StartupOrchestrator.Abstractions;

public interface IActivatorOrchestrator
{
    bool IsActive(IHostApplicationBuilder builder);
}
