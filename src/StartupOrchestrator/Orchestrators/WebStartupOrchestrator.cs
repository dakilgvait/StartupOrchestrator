using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StartupOrchestrator.Orchestrators;

public class WebStartupOrchestrator : BaseStartupOrchestrator<WebApplicationBuilder, WebApplication>
{
    public WebStartupOrchestrator(string[] args)
    {
        _ = Create(() => WebApplication.CreateBuilder(args));
    }

    public override WebStartupOrchestrator Configure(Action<IApplicationBuilder> action)
    {
        _ = base.Configure(action);

        return this;
    }

    public override WebStartupOrchestrator ConfigureServices(Action<IServiceCollection, WebApplicationBuilder> action)
    {
        _ = base.ConfigureServices(action);

        return this;
    }

    public override WebStartupOrchestrator InstallModule<TModule>()
    {
        _ = base.InstallModule<TModule>();

        return this;
    }

    public WebStartupOrchestrator Build()
    {
        _ = Build(x => x.Build());

        return this;
    }

    public WebStartupOrchestrator Run()
    {
        _ = Run(x => x.Run());

        return this;
    }
}
