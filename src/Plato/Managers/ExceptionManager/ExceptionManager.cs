// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Plato.Interfaces;
using Plato.Miscellaneous;
using Plato.Strings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Plato.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Interfaces.IExceptionManager" />
    public class ExceptionManager : IExceptionManager
    {
        private readonly ExceptionManagerSettings _settings;
        private readonly ILogger<ExceptionManager> _logger;
        private readonly IEnumerable<IExceptionHandler> _exceptionHandlers;
        private readonly TimeSpan _eventTracking;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionManager" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="exceptionHandlers">The exception handlers.</param>
        public ExceptionManager(
            IOptions<ExceptionManagerSettings> settings,
            ILogger<ExceptionManager> logger,
            IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            Guard.AgainstNull(() => settings);
            Guard.AgainstNull(() => logger);
            Guard.AgainstNull(() => exceptionHandlers);

            _settings = settings.Value;
            _logger = logger;
            _exceptionHandlers = exceptionHandlers;
            _eventTracking = new TimeSpan(0, _settings.EventTrackingInterval, 0);
        }

        /// <summary>
        /// Handles the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="ignoreTracking">if set to <c>true</c> [ignore tracking].</param>
        public void Handle(Exception ex, NameValueCollection additionalInfo = null, bool ignoreTracking = false)
        {
            if (!TimeEventTracker.CanEvent(ex.Message, _eventTracking, out int occurrences) || ignoreTracking)
            {
                return;
            }

            additionalInfo = additionalInfo ?? new NameValueCollection();
            additionalInfo["TrackingId"] = SequentialGuid.NewGuid().ToString();
            additionalInfo["Time (UTC)"] = DateTime.UtcNow.ToString();

            if (occurrences != 0)
            {
                additionalInfo["Occurrences"] = occurrences.ToString("N0");
            }

            foreach (var handler in _exceptionHandlers)
            {
                try
                {
                    handler.Handle(ex, additionalInfo);
                }
                catch (Exception ex2)
                {
                    // one of the handlers screwed up.

                    var error = $"Exception Handler: '{handler.Name}' threw an unhandled exception.";
                    var handlerException = new AggregateException(new Exception(error, ex2), ex);

                    var errorMessage = ExceptionFormatter.ConstructMessage(handlerException, additionalInfo);
                    _logger.LogError(errorMessage);
                }
            }
        }

        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="ignoreTracking">if set to <c>true</c> [ignore tracking].</param>
        /// <returns></returns>
        public Task HandleAsync(Exception ex, NameValueCollection additionalInfo = null, bool ignoreTracking = false)
        {
            Handle(ex, additionalInfo, ignoreTracking);

            return Task.CompletedTask;
        }
    }
}
