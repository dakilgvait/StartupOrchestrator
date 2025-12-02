using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StartupOrchestrator.Abstractions;

public interface IStartupOrchestrator<TBuilder, TApplication>
{
    IStartupOrchestrator<TBuilder, TApplication> InstallModule<TModule>();
    IStartupOrchestrator<TBuilder, TApplication> Create(Func<TBuilder> func);
    IStartupOrchestrator<TBuilder, TApplication> ConfigureServices(Action<IServiceCollection, TBuilder> action);
    IStartupOrchestrator<TBuilder, TApplication> Configure(Action<IApplicationBuilder> action);
    IStartupOrchestrator<TBuilder, TApplication> Build(Func<TBuilder, TApplication> func);
    IStartupOrchestrator<TBuilder, TApplication> Run(Action<TApplication> func);
}
