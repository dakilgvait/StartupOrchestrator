using Microsoft.Extensions.DependencyInjection;

namespace StartupPack.Domain;

public interface IConfigureServicesOrchestrator
{
    void ConfigureServices(IServiceCollection services);
}
