using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupOrchestrator.Abstractions.Components;

namespace StartupOrchestrator.Abstractions.Orchestration
{
    public interface IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication>
        where TApplicationBuilder : IHostApplicationBuilder
        where TApplication : IApplicationBuilder
    {
        IStartupApplicationOrchestrator<TApplication> Build(Func<TApplicationBuilder, TApplication> function);

        IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> Configure(Action<IApplicationBuilder, IServiceProvider> action);

        IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> ConfigureServices(Action<IServiceCollection, IConfiguration, IServiceProvider> action);

        IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> Create(Func<TApplicationBuilder> function);

        IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> InstallComponent<TComponent>()
            where TComponent : class, IStartupComponent;
    }
}
