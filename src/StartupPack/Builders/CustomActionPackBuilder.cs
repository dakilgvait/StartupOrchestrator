using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace StartupPack;

public class CustomActionPackBuilder : IStartupPackBuilder
{
    private readonly CustomActionPackModel _packModel;

    public CustomActionPackBuilder()
    {
        _packModel = new CustomActionPackModel();
    }

    public IStartupPack Build(IServiceProvider provider)
    {
        var pack = new CustomActionPack(provider, _packModel);

        return pack;
    }

    public void SetupServicesAction(Action<IServiceCollection> action)
    {
        _packModel.ActionAdd = action;
    }

    public void SetupIsActiveFunction(Func<bool> func)
    {
        _packModel.FunctionIsActive = func;
    }

    public void SetupKey(string key)
    {
        _packModel.Name = key;
    }

    public void SetupApplicationAction(Action<IApplicationBuilder> action)
    {
        _packModel.ActionUse = action;
    }
}
