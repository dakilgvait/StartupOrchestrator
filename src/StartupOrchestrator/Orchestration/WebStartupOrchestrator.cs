using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupOrchestrator.Abstractions.Components;

namespace StartupOrchestrator.Orchestration
{
    public partial class WebStartupOrchestrator : BaseStartupOrchestrator<WebApplicationBuilder, WebApplication>
    {
        public void BuildThenRun()
        {
            _ = Build(builder => builder.Build());
            Run(application => application.Run());
        }

        public new WebStartupOrchestrator Configure(Action<IApplicationBuilder, IServiceProvider> action)
        {
            _ = base.Configure(action);

            return this;
        }

        public new WebStartupOrchestrator ConfigureServices(Action<IServiceCollection, IConfiguration, IServiceProvider> action)
        {
            _ = base.ConfigureServices(action);

            return this;
        }

        public WebStartupOrchestrator Create(string[] args)
        {
            _ = Create(() => WebApplication.CreateBuilder(args));

            OrchestratorServices.AddScoped<IHostBuilder>((sp) => Builder.Host);

            return this;
        }

        public new WebStartupOrchestrator InstallComponent<TComponent>()
            where TComponent : class, IStartupComponent
        {
            _ = base.InstallComponent<TComponent>();

            return this;
        }
    }

    public partial class WebStartupOrchestrator
    {
        public static WebStartupOrchestrator CreateOrchestrator(string[] args)
        {
            return new WebStartupOrchestrator().Create(args);
        }
    }
}
