// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Autofac;
using Microsoft.Extensions.Logging;
using Plato.Autofac;
using Plato.Interfaces;
using Plato.Miscellaneous;
using System;
using System.Threading.Tasks;

namespace Plato.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Interfaces.ISingleSparkManager" />
    public class SingleSparkManager : ISingleSparkManager
    {
        private readonly ILogger<SingleSparkManager> _logger;
        private readonly IContainer _container;
        private readonly ISingleSparkManagerLifeCycle _sparkLifeCycle;
        private readonly IExceptionManager _exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleSparkManager"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="container">The container.</param>
        /// <param name="sparkLifeCycle">The spark life cycle.</param>
        /// <param name="exceptionManager">The exception manager.</param>
        public SingleSparkManager(
            ILogger<SingleSparkManager> logger,
            IContainer container,
            ISingleSparkManagerLifeCycle sparkLifeCycle,
            IExceptionManager exceptionManager)
        {
            Guard.AgainstNull(() => logger);
            Guard.AgainstNull(() => container);
            Guard.AgainstNull(() => sparkLifeCycle);
            Guard.AgainstNull(() => exceptionManager);

            _logger = logger;
            _container = container;
            _sparkLifeCycle = sparkLifeCycle;
            _exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Ignites this instance.
        /// </summary>
        public void Ignite<TProcessor>() where TProcessor : ISingleSparkIgniteProcessor
        {
            // this Task will start in the background
            // it will only end if the app terminates

            Task.Run(async () =>
            {
                _logger.LogInformation($"Igniting Spark");

                var graceful = true;

                try
                {
                    while (true)
                    {
                        if (await _sparkLifeCycle.KeepAliveAsync() == false)
                        {
                            _logger.LogInformation("Termination signal detected.");
                            break;
                        }

                        try
                        {
                            // ensure that the processor always has a new set of scoped dependencies 

                            var scope = (ILifetimeScope)null;
                            using (scope = _container.BeginLifetimeScope(sb =>
                            {
                                sb.Register(ctx => new DependencyFactory(scope)).As<IDependencyFactory>().SingleInstance();
                            }))
                            {
                                var serviceProcess = scope.Resolve<TProcessor>();
                                await serviceProcess.ProcessAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            await _exceptionManager.HandleAsync(ex);
                        }
                        finally
                        {
                            await _sparkLifeCycle.RelaxAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    await _exceptionManager.HandleAsync(ex);

                    _logger.LogWarning("Process terminated abruptly.");
                    graceful = false;
                }

                if (graceful)
                {
                    _logger.LogInformation("Process terminated gracefully!");
                }
            }).GetAwaiter();
        }
    }
}
