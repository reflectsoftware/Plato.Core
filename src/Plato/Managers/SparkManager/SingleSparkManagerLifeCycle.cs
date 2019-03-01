// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Plato.Interfaces;
using Plato.Miscellaneous;
using System;
using System.Threading.Tasks;

namespace Plato.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Interfaces.ISingleSparkManagerLifeCycle" />
    public class SingleSparkManagerLifeCycle : ISingleSparkManagerLifeCycle
    {
        #region declarations
        enum LifeCycleState
        {
            Running,
            Terminating,
            Terminated
        }
        #endregion declarations

        private readonly SingleSparkManagerLifeCycleSettings _settings;
        private readonly ILogger<SingleSparkManagerLifeCycle> _logger;

        private DateTimeOffset _startPeriod;
        private LifeCycleState _cycleState;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleSparkManagerLifeCycleSettings" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="logger">The logger.</param>
        public SingleSparkManagerLifeCycle(
            IOptions<SingleSparkManagerLifeCycleSettings> settings,
            ILogger<SingleSparkManagerLifeCycle> logger)
        {
            Guard.AgainstNull(() => settings);
            Guard.AgainstNull(() => logger);

            _settings = settings.Value;
            _logger = logger;
            _cycleState = LifeCycleState.Running;
        }

        /// <summary>
        /// Keeps the alive asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<bool> KeepAliveAsync()
        {
            if (_cycleState == LifeCycleState.Terminating)
            {
                _cycleState = LifeCycleState.Terminated;
                return Task.FromResult(false);
            }

            _startPeriod = DateTimeOffset.UtcNow;

            return Task.FromResult(true);
        }
        /// <summary>
        /// Determines whether [is alive asynchronous].
        /// </summary>
        /// <returns></returns>
        public Task<bool> IsAliveAsync()
        {
            var elapsedTime = DateTimeOffset.UtcNow.Subtract(_startPeriod);
            var isAlive = elapsedTime.TotalMinutes <= _settings.KeepAliveWindow;

            return Task.FromResult(isAlive);
        }

        /// <summary>
        /// Terminates the asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task TerminateAsync()
        {
            _cycleState = LifeCycleState.Terminating;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Determines whether [is terminated asynchronous].
        /// </summary>
        /// <returns></returns>
        public Task<bool> IsTerminatedAsync()
        {
            return Task.FromResult(_cycleState == LifeCycleState.Terminated);
        }

        /// <summary>
        /// Relaxes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task RelaxAsync()
        {
            if (_cycleState != LifeCycleState.Running || _settings.RelaxPeriod == 0)
            {
                return;
            }

            var elapsedTime = DateTimeOffset.UtcNow.Subtract(_startPeriod);
            var relax = _settings.RelaxPeriod - elapsedTime.TotalMilliseconds;

            if (relax > 0)
            {
                // passively delay - watch for termination indicators
                var elaspsed = 0;
                while (_cycleState == LifeCycleState.Running && elaspsed < relax)
                {
                    await Task.Delay(50);
                    elaspsed += 50;
                }
            }
        }
    }
}
