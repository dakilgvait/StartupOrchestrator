using Microsoft.AspNetCore.Builder;

namespace StartupOrchestrator.Abstractions;

public interface IConfigureOrchestrator
{
    void Configure(IApplicationBuilder app);
}
