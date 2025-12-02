using Microsoft.Extensions.DependencyInjection;

namespace StartupOrchestrator.Abstractions;

public interface IConfigureServicesOrchestrator
{
    void ConfigureServices(IServiceCollection services);
}
