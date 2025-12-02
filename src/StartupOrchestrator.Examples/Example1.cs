using StartupOrchestrator.Orchestrators;

namespace StartupOrchestrator.Examples;

public class Example1
{
    public static void Main(params string[] args)
    {
        var orchestrator = new WebStartupOrchestrator(args)
            .ConfigureServices((services, builder) => { })
            .Configure(app => { })
            .BuildThenRun();
    }
}
