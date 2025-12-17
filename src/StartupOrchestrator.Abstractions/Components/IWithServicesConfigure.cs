using Microsoft.Extensions.DependencyInjection;

namespace StartupOrchestrator.Abstractions.Components
{
    public interface IWithServicesConfigure
    {
        void ConfigureServices(IServiceCollection services);
    }
}
