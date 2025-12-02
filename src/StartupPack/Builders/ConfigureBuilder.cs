using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StartupPack;

public class ConfigureBuilder : IConfigureBuilder
{
    private readonly IEnumerable<IStartupPack> _packs;

    public ConfigureBuilder(IEnumerable<IStartupPack> packs)
    {
        _packs = packs;
    }

    public void Build(IApplicationBuilder application)
    {
        var activeStartups = _packs.Select(x => new
        {
            index = x.GetApplicationIndex(),
            pack = x
        }).OrderByDescending(x => x.index.HasValue)
            .OrderBy(x => x.index)
            .Select(x => x.pack)
            .ToList();

        activeStartups.ForEach(x => x.Configure(application));
    }
}

public class StartupOrchestrator<TBuilder, TApplication>
    where TBuilder : IHostApplicationBuilder
    where TApplication : IApplicationBuilder
{
    public TBuilder Builder { get; private set; }
    public TApplication Application { get; private set; }
    public ICollection<IStartupPack> Modules { get; } = new List<IStartupPack>(20);

    public StartupOrchestrator<TBuilder, TApplication> InstallModule<TModule>()
        where TModule : IStartupPack, new()
    {
        Modules.Add(new TModule());

        

         return this;
    }

    public StartupOrchestrator<TBuilder, TApplication> Create(Func<TBuilder> func)
    {
        Builder = func.Invoke();

        return this;
    }

    public StartupOrchestrator<TBuilder, TApplication> ConfigureServices(Action<IServiceCollection, TBuilder> action)
    {
        action.Invoke(Builder.Services, Builder);

        return this;
    }
    public StartupOrchestrator<TBuilder, TApplication> Configure(Action<IApplicationBuilder> action)
    {
        action.Invoke(Application);

        return this;
    }

    public StartupOrchestrator<TBuilder, TApplication> Build(Func<TBuilder, TApplication> func)
    {
        Application = func.Invoke(Builder);

        return this;
    }

    public StartupOrchestrator<TBuilder, TApplication> Run(Action<TApplication> func)
    {
        func.Invoke(Application);

        return this;
    }
}
