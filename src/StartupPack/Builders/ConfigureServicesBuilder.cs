using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace StartupPack;

public class ConfigureServicesBuilder : IConfigureServicesBuilder
{
    private readonly IServiceCollection _packServices;

    public ConfigureServicesBuilder()
    {
        _packServices = InitializeServices();
    }

    protected IServiceCollection InitializeServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IPackIndexer, PackIndexer>();
        services.AddSingleton<IPackActivator, LocalhostPackDeactivator>();

        return services;
    }

    public IConfigureBuilder Build(IServiceCollection services)
    {
        var provider = _packServices.BuildServiceProvider();

        var activeStartups = provider.GetServices<IStartupPackBuilder>()
            .Select(x => x.Build(provider))
            .Concat(provider.GetServices<IStartupPack>())
            .Where(x => x.GetIsActive())
            .ToArray();

        var orderedStartups = activeStartups.Select(x => new
        {
            index = x.GetServicesIndex(),
            pack = x
        }).OrderByDescending(x => x.index.HasValue)
            .OrderBy(x => x.index)
            .Select(x => x.pack)
            .ToList();

        orderedStartups.ForEach(x => x.ConfigureServices(services));

        return new ConfigureBuilder(activeStartups);
    }

    public void Install(Action<IStartupPackBuilder> action)
    {
        var builder = new CustomActionPackBuilder();
        action.Invoke(builder);
        _packServices.AddSingleton<IStartupPackBuilder>(builder);
    }

    public void Register(Action<IServiceCollection> action)
    {
        action.Invoke(_packServices);
    }

    public void InstallBuilder<TBuilder>() where TBuilder : class, IStartupPackBuilder
    {
        _packServices.AddSingleton<IStartupPackBuilder, TBuilder>();
    }

    public void InstallPack<TPack>() where TPack : class, IStartupPack
    {
        _packServices.AddSingleton<IStartupPack, TPack>();
    }
}
