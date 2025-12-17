using Microsoft.Extensions.Hosting;
using StartupOrchestrator.Abstractions.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupOrchestrator.Activation
{
    public class LocalhostActivator : IStartupActivator
    {
        public const string LOCALHOST = "Localhost";

        public LocalhostActivator(IHostEnvironment environment)
        {
            Environment = environment;
        }

        protected IHostEnvironment Environment { get; }

        public bool GetIsActive()
        {
            return Environment.IsEnvironment(LOCALHOST);
        }
    }
}
