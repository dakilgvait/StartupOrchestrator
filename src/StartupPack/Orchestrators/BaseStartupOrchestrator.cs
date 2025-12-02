using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupPack.Domain;

namespace StartupPack.Orchestrators;

public class BaseStartupOrchestrator<TBuilder, TApplication> : IStartupOrchestrator<TBuilder, TApplication>
    where TBuilder : IHostApplicationBuilder
    where TApplication : IApplicationBuilder
{
    public TBuilder Builder { get; private set; } = default!;
    public TApplication Application { get; private set; } = default!;
    public ICollection<Type> Modules { get; private set; } = default!;

    public virtual IStartupOrchestrator<TBuilder, TApplication> Build(Func<TBuilder, TApplication> func)
    {
        var instances = Modules.Select(x => Activator.CreateInstance(x)).ToList();
        instances.ForEach(x => (x as IInitializerOrchestrator)?.Initialize(Builder));

        var activeInstances = instances.Select(x => new
        {
            instance = x,
            isActive = (x as IActivatorOrchestrator)?.IsActive(Builder)
        }).Where(x => !x.isActive.HasValue || x.isActive.Value)
            .Select(x => x.instance)
            .ToArray();

        activeInstances.Select(x => x as IConfigureServicesOrchestrator)
            .Where(x => x is not null).ToList()
            .ForEach(x => x?.ConfigureServices(Builder.Services));

        Application = func.Invoke(Builder);

        activeInstances.Select(x => x as IConfigureOrchestrator)
            .Where(x => x is not null).ToList()
            .ForEach(x => x!.Configure(Application));

        return this;
    }

    public virtual IStartupOrchestrator<TBuilder, TApplication> Configure(Action<IApplicationBuilder> action)
    {
        action.Invoke(Application);

        return this;
    }

    public virtual IStartupOrchestrator<TBuilder, TApplication> ConfigureServices(Action<IServiceCollection, TBuilder> action)
    {
        action.Invoke(Builder.Services, Builder);

        return this;
    }

    public virtual IStartupOrchestrator<TBuilder, TApplication> Create(Func<TBuilder> func)
    {
        Builder = func.Invoke();

        return this;
    }

    public virtual IStartupOrchestrator<TBuilder, TApplication> InstallModule<TModule>()      
    {
        var existList = Modules ?? new List<Type>(0);
        var newList = new List<Type>(existList.Count + 1);
        newList.AddRange(existList);
        newList.Add(typeof(TModule));

        Modules = newList;

        return this;
    }

    public virtual IStartupOrchestrator<TBuilder, TApplication> Run(Action<TApplication> func)
    {
        func.Invoke(Application);

        return this;
    }
}
