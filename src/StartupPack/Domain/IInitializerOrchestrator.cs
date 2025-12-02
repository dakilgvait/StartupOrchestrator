using Microsoft.Extensions.Hosting;

namespace StartupPack.Domain;

public interface IInitializerOrchestrator
{
    void Initialize(IHostApplicationBuilder builder);
}
