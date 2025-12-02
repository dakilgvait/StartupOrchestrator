using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StartupPack;

public interface IStartupPack
{
    void ConfigureServices(IServiceCollection services);
    void Configure(IApplicationBuilder application);
    bool GetIsActive();
    int? GetServicesIndex();
    int? GetApplicationIndex();
}
