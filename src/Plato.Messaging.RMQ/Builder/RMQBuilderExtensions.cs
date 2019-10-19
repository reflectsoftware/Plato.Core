// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plato.Messaging.Enums;
using Plato.Messaging.Exceptions;
using Plato.Messaging.RMQ.Factories;
using Plato.Messaging.RMQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plato.Messaging.RMQ.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class RMQBuilderExtensions
    {
        private static readonly Dictionary<Type, RMQQueueBuilderOptions> _Consumers;

        /// <summary>
        /// Initializes the <see cref="RMQBuilderExtensions"/> class.
        /// </summary>
        static RMQBuilderExtensions()
        {
            _Consumers = new Dictionary<Type, RMQQueueBuilderOptions>();
        }

        /// <summary>
        /// Adds the RMQ bound consumers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configure">The configure.</param>
        /// <returns></returns>
        public static IServiceCollection AddRMQBoundConsumers<T>(this IServiceCollection services, Action<RMQQueueBuilderOptions> configure) where T : IRMQBoundConsumer
        {
            var type = typeof(T);
            var options = new RMQQueueBuilderOptions();

            configure(options);

            _Consumers[type] = options;

            services.AddScoped(type);

            return services;
        }

        /// <summary>
        /// Uses the RMQ bound consumers.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static void UseRMQBoundConsumers(this IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            foreach (var type in _Consumers.Keys)
            {
                Task.Run(async () =>
                {
                    var logFactory = serviceProvider.GetService<ILoggerFactory>();
                    var logger = logFactory.CreateLogger(type);
                    var consumerFactory = new RMQConsumerFactory(new RMQConnectionFactory());
                    var options = _Consumers[type];

                    var consumer = typeof(IRMQBoundConsumerText).IsAssignableFrom(type)
                        ? (IRMQConsumer)consumerFactory.CreateText(options.ConnectionSettings, options.QueueSettings)
                        : consumerFactory.CreateBytes(options.ConnectionSettings, options.QueueSettings);

                    using (consumer)
                    {
                        consumer.Mode = ConsumerMode.OnNoMessage_ReturnNull;

                        while (cancellationToken.IsCancellationRequested == false)
                        {
                            try
                            {
                                try
                                {
                                    if (consumer is IRMQConsumerText)
                                    {
                                        var message = (consumer as IRMQConsumerText).Receive(1000);
                                        if (message != null)
                                        {
                                            using (var scope = serviceProvider.CreateScope())
                                            {
                                                var loopConsumer = scope.ServiceProvider.GetService(type) as IRMQBoundConsumerText;
                                                await loopConsumer.OnMessageAsync(message);
                                                message.Acknowledge();
                                            }
                                        }
                                    }
                                    else // bytes
                                    {
                                        var message = (consumer as IRMQConsumerBytes).Receive(1000);
                                        if (message != null)
                                        {
                                            using (var scope = serviceProvider.CreateScope())
                                            {
                                                var loopConsumer = scope.ServiceProvider.GetService(type) as IRMQBoundConsumerBytes;
                                                await loopConsumer.OnMessageAsync(message);
                                                message.Acknowledge();
                                            }
                                        }
                                    }
                                }
                                catch (TimeoutException)
                                {
                                }
                                catch (MessageException ex)
                                {
                                    switch (ex.ExceptionCode)
                                    {
                                        case MessageExceptionCode.ExclusiveLock:
                                            await Task.Delay(1000);
                                            break;

                                        default:
                                            throw;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                consumer.ClearCacheBuffer();
                                logger.LogError(ex, "RMQ Consumer Error.");

                                try
                                {
                                    options?.OnException(ex);
                                }
                                catch (Exception ex2)
                                {
                                    logger.LogError(ex2, "RMQ Consumer Error (OnException Handler fault).");
                                }
                            }
                        }
                    }
                }).GetAwaiter();
            }
        }

        /// <summary>
        /// Uses the RMQ bound consumers.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRMQBoundConsumers(this IApplicationBuilder app)
        {
            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();

            app.ApplicationServices.UseRMQBoundConsumers(lifetime.ApplicationStopping);

            return app;
        }
    }
}
