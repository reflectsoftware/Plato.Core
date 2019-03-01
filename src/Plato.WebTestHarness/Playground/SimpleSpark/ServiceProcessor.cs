using Microsoft.Extensions.Logging;
using Plato.Autofac;
using Plato.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plato.WebTestHarness.Playground.SimpleSpark
{
    public class ServiceProcessor : IServiceProcessor
    {
        private readonly ILogger<ServiceProcessor> _logger;
        private readonly IDependencyFactory _dependencyFactory;

        public ServiceProcessor(
            ILogger<ServiceProcessor> logger,
            IDependencyFactory dependencyFactory)
        {
            Guard.AgainstNull(() => logger);
            Guard.AgainstNull(() => dependencyFactory);

            _logger = logger;
            _dependencyFactory = dependencyFactory;
        }

        public Task ProcessAsync()
        {
            _logger.LogInformation("Processor");

            var state = false;
            if (state)
            {
                throw new Exception("WTF!...");
            }

            return Task.CompletedTask;
        }
    }
}
