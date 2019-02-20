// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.RMQ.Interfaces;
using Plato.Messaging.RMQ.Pool;

namespace Plato.Messaging.RMQ.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.RMQ.Interfaces.IRMQPoolFactory" />
    public class RMQPoolFactory : IRMQPoolFactory
    {
        /// <summary>
        /// Creates the sender receiver factory.
        /// </summary>
        /// <returns></returns>
        protected IRMQSenderReceiverFactory CreateSenderReceiverFactory()
        {
            var connectionFactory = new RMQConnectionFactory();
            var consumerFactory = new RMQConsumerFactory(connectionFactory);
            var producerFactory = new RMQProducerFactory(connectionFactory);
            var subscriberFactory = new RMQSubscriberFactory(connectionFactory);
            var publisherFactory = new RMQPublisherFactory(connectionFactory);
            var senderReceiverFactory = new RMQSenderReceiverFactory(consumerFactory, producerFactory, subscriberFactory, publisherFactory);

            return senderReceiverFactory;
        }

        /// <summary>
        /// Creates the asynchronous pool.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="maxGrowSize">Maximum size of the grow.</param>
        /// <returns></returns>
        public IRMQPoolAsync CreateAsyncPool(IRMQConfigurationManager configuration, int maxGrowSize = 3)
        {
            var senderReceiverFactory = CreateSenderReceiverFactory();
            var pool = new RMQPoolAsync(configuration, senderReceiverFactory, maxGrowSize);

            return pool;
        }

        /// <summary>
        /// Creates the pool.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="maxGrowSize">Maximum size of the grow.</param>
        /// <returns></returns>
        public IRMQPool CreatePool(IRMQConfigurationManager configuration, int maxGrowSize = 3)
        {
            var senderReceiverFactory = CreateSenderReceiverFactory();
            var pool = new RMQPool(configuration, senderReceiverFactory, maxGrowSize);

            return pool;
        }
    }
}
