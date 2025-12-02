using Microsoft.AspNetCore.Builder;

namespace StartupPack.Domain;

public interface IConfigureOrchestrator
{
    void Configure(IApplicationBuilder app);
}
