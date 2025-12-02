using Microsoft.Extensions.Hosting;

namespace StartupPack.Domain;

public interface IActivatorOrchestrator
{
    bool IsActive(IHostApplicationBuilder builder);
}
