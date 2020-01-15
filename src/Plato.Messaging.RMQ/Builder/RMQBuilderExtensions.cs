// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plato.Messaging.Enums;
using Plato.Messaging.Exceptions;
using Plato.Messaging.RMQ.Factories;
using Plato.Messaging.RMQ.Interfaces;
using Plato.Miscellaneous;
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
        private static readonly Dictionary<Type, RMQBuilderConsumerOptions> _Consumers;

        /// <summary>
        /// Initializes the <see cref="RMQBuilderExtensions"/> class.
        /// </summary>
        static RMQBuilderExtensions()
        {
            _Consumers = new Dictionary<Type, RMQBuilderConsumerOptions>();
        }

        /// <summary>
        /// Adds the RMQ bound consumers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configure">The configure.</param>
        /// <returns></returns>
        public static IServiceCollection AddRMQBoundConsumer<T>(this IServiceCollection services, Action<RMQBuilderConsumerOptions> configure) where T : IRMQBoundConsumer
        {
            var type = typeof(T);
            var options = new RMQBuilderConsumerOptions();

            configure(options);

            Guard.AgainstNull(options.ConnectionSettings, nameof(options.ConnectionSettings));
            Guard.AgainstNull(options.QueueSettings, nameof(options.QueueSettings));

            _Consumers[type] = options;

            services.AddScoped(type);

            return services;
        }

        /// <summary>
        /// Adds the RMQ bound subscriber.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configure">The configure.</param>
        /// <returns></returns>
        public static IServiceCollection AddRMQBoundSubscriber<T>(this IServiceCollection services, Action<RMQBuilderSubscriptionOptions> configure) where T : IRMQBoundConsumer
        {
            var type = typeof(T);
            var options = new RMQBuilderSubscriptionOptions();

            configure(options);

            Guard.AgainstNull(options.ConnectionSettings, nameof(options.ConnectionSettings));
            Guard.AgainstNull(options.ExchangeSettings, nameof(options.ExchangeSettings));
            Guard.AgainstNull(options.QueueSettings, nameof(options.QueueSettings));

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
            var consumerFactory = new RMQConsumerFactory(new RMQConnectionFactory());
            var subcriberFactory = new RMQSubscriberFactory(new RMQConnectionFactory());
            var logFactory = serviceProvider.GetService<ILoggerFactory>();

            foreach (var type in _Consumers.Keys)
            {
                Task.Run(async () =>
                {                    
                    var logger = logFactory.CreateLogger(type);                    
                    var options = _Consumers[type];
                    var consumer = (IRMQConsumer)null;
                    var isTextConsumer = typeof(IRMQBoundConsumerText).IsAssignableFrom(type);

                    if (options is RMQBuilderSubscriptionOptions)
                    {
                        var subOptions = (options as RMQBuilderSubscriptionOptions);

                        if (isTextConsumer)
                        {
                            consumer = subcriberFactory.CreateText(subOptions.ConnectionSettings, subOptions.ExchangeSettings, subOptions.QueueSettings);
                        }
                        else
                        {
                            consumer = subcriberFactory.CreateBytes(subOptions.ConnectionSettings, subOptions.ExchangeSettings, subOptions.QueueSettings);
                        }
                    }
                    else // standard consumer
                    {

                        if (isTextConsumer)
                        {
                            consumer = consumerFactory.CreateText(options.ConnectionSettings, options.QueueSettings);
                        }
                        else
                        {
                            consumer = consumerFactory.CreateBytes(options.ConnectionSettings, options.QueueSettings);
                        }
                    }

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
                                                await loopConsumer.OnMessageAsync(message, cancellationToken);
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
                                    var aggregateException = new AggregateException("RMQ Consumer Error (OnException Handler fault).", new[] { ex, ex2 });                                    
                                    logger.LogError(aggregateException, aggregateException.Message);
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
