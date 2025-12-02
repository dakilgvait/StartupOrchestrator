using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StartupPack;

public class CustomActionPack : IStartupPack
{
    private readonly CustomActionPackModel _packModel;
    private readonly IPackIndexer? _packIndexer;
    private readonly IPackActivator? _packActivator;

    public CustomActionPack(IServiceProvider provider, CustomActionPackModel model)
    {
        _packIndexer = provider.GetService<IPackIndexer>();
        _packActivator = provider.GetService<IPackActivator>();
        _packModel = model;
    }

    public bool GetIsActive()
    {
        if (_packModel.FunctionIsActive != null)
        {
            return _packModel.FunctionIsActive.Invoke();
        }

        return _packActivator?.IsActive(_packModel.Name) ?? true;
    }

    public void Configure(IApplicationBuilder application)
    {
        _packModel.ActionUse?.Invoke(application);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        _packModel.ActionAdd?.Invoke(services);
    }

    public int? GetServicesIndex()
    {
        return _packIndexer?.GetApplicationIndex(_packModel.Name);
    }

    public int? GetApplicationIndex()
    {
        return _packIndexer?.GetServicesIndex(_packModel.Name);
    }
}
