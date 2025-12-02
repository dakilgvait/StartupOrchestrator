using System;
using StartupPack;

namespace Examples.Modules;

public class TestModule : IStartupPack
{
    public void Configure(IApplicationBuilder application)
    {
        throw new NotImplementedException();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public int? GetApplicationIndex()
    {
        throw new NotImplementedException();
    }

    public bool GetIsActive()
    {
        throw new NotImplementedException();
    }

    public int? GetServicesIndex()
    {
        throw new NotImplementedException();
    }
}
