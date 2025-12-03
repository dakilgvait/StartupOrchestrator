using StartupOrchestrator.Orchestrators;

namespace StartupOrchestrator.Examples;

public class WebStartupOrchestrator_Example
{
    public static void Main(params string[] args)
    {
        var orchestrator = new WebStartupOrchestrator(args)
            .ConfigureServices((services, builder) => { })
            .Configure(app => { })
            .BuildThenRun();
    }
}
