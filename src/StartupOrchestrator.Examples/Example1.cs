using StartupOrchestrator.Orchestration;

namespace StartupOrchestrator.Examples;

public static class Example1
{
    public static void Main(params string[] args)
    {
        WebStartupOrchestrator
            .CreateOrchestrator(args)
            .ConfigureServices((services, configuration, startupProvider) => { })
            .Configure((app, startupProvider) => { })
            .BuildThenRun();
    }
}
