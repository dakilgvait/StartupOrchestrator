using Microsoft.AspNetCore.Builder;

namespace StartupOrchestrator.Abstractions.Components
{
    public interface IWithApplicationConfigure
    {
        void Configure(IApplicationBuilder application);
    }
}
