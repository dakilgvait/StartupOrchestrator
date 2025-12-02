using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StartupPack;

public interface IStartupPackBuilder
{
    void SetupKey(string key);
    void SetupServicesAction(Action<IServiceCollection> action);
    void SetupApplicationAction(Action<IApplicationBuilder> action);
    void SetupIsActiveFunction(Func<bool> func);
    IStartupPack Build(IServiceProvider provider);
}
