using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupOrchestrator.Abstractions.Activation;
using StartupOrchestrator.Abstractions.Components;
using StartupOrchestrator.Abstractions.Orchestration;
using StartupOrchestrator.Activation;
using StartupOrchestrator.Synchronization;

namespace StartupOrchestrator.Orchestration
{
    public class BaseStartupOrchestrator<TApplicationBuilder, TApplication> : IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication>, IStartupApplicationOrchestrator<TApplication>
        where TApplicationBuilder : class, IHostApplicationBuilder
        where TApplication : class, IApplicationBuilder
    {
        public TApplication Application { get; private set; } = default!;
        public TApplicationBuilder Builder { get; private set; } = default!;
        protected Action<IApplicationBuilder, IServiceProvider> OnConfigure { get; private set; } = default!;
        protected Action<IServiceCollection, IConfiguration, IServiceProvider> OnConfigureServices { get; private set; } = default!;
        protected IServiceCollection OrchestratorServices { get; } = new ServiceCollection();

        public virtual IStartupApplicationOrchestrator<TApplication> Build(Func<TApplicationBuilder, TApplication> function)
        {
            using var provider = OrchestratorServices.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var components = ConfigureServices(scope.ServiceProvider);
            Application = ConfigureApplication(components, function, scope.ServiceProvider);

            return this;
        }

        public virtual IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> Configure(Action<IApplicationBuilder, IServiceProvider> action)
        {
            OnConfigure = action;

            return this;
        }

        public virtual IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> ConfigureServices(Action<IServiceCollection, IConfiguration, IServiceProvider> action)
        {
            OnConfigureServices = action;

            return this;
        }

        public virtual IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> Create(Func<TApplicationBuilder> function)
        {
            var builder = Builder = function.Invoke();

            OrchestratorServices.AddScoped((sp) => builder);
            OrchestratorServices.AddScoped<IHostApplicationBuilder>((sp) => builder);
            OrchestratorServices.AddScoped<IConfiguration>((sp) => builder.Configuration);
            OrchestratorServices.AddScoped<IConfigurationBuilder>((sp) => builder.Configuration);
            OrchestratorServices.AddScoped((sp) => builder.Environment);
            OrchestratorServices.AddScoped((sp) => builder.Services);

            OrchestratorServices.AddScoped<LocalhostActivator>();
            OrchestratorServices.AddScoped<BarrierActionRegistry>();

            return this;
        }

        public virtual IStartupApplicationBuilderOrchestrator<TApplicationBuilder, TApplication> InstallComponent<TComponent>()
            where TComponent : class, IStartupComponent
        {
            OrchestratorServices.AddScoped<TComponent>();
            OrchestratorServices.AddScoped<IStartupComponent>((sp) => sp.GetRequiredService<TComponent>());

            return this;
        }

        public virtual void Run(Action<TApplication> action)
        {
            action.Invoke(Application);
        }

        protected virtual TApplication ConfigureApplication(IEnumerable<IStartupComponent> components, Func<TApplicationBuilder, TApplication> func, IServiceProvider provider)
        {
            var application = func.Invoke(Builder);

            components.Select(x => new
            {
                sorter = (x as IWithSorter)?.GetSorter()
                    ?? (x as IStartupSorter),
                configurator = x as IWithApplicationConfigure,
                type = x.GetType()
            }).Where(x => x.configurator is not null)
            .OrderBy(x => x.sorter?.GetOrder(x.type))
            .Select(x => x.configurator!).ToList()
            .ForEach(x => x.Configure(application));

            OnConfigure(application, provider);

            return application;
        }

        protected virtual IEnumerable<IStartupComponent> ConfigureServices(IServiceProvider provider)
        {
            var components = provider.GetRequiredService<IEnumerable<IStartupComponent>>()
                .Where(x => ((x as IWithActivator)?.GetActivator()
                    ?? x as IStartupActivator)?.GetIsActive() ?? true)
                .Select(x => x).ToArray();

            components.Select(x => x as IWithServicesConfigure)
                .Where(x => x is not null)
                .Select(x => x!).ToList()
                .ForEach(x => x.ConfigureServices(Builder.Services));

            OnConfigureServices(Builder.Services, Builder.Configuration, provider);

            return components;
        }
    }
}
