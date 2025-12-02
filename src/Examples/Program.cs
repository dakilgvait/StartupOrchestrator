using Examples.Modules;
using StartupPack.Orchestrators;

var builder = new WebStartupOrchestrator(args)
    .InstallModule<TestModule>()
    .ConfigureServices((services, appBuilder) =>
    {

    }).Configure(appBuilder =>
    {

    }).Build()
    .Run();