// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Newtonsoft.Json;
using Plato.Messaging.Enums;
using Plato.Messaging.Exceptions;
using Plato.Messaging.Interfaces;
using Plato.Messaging.RMQ.Interfaces;
using Plato.Miscellaneous;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plato.Messaging.RMQ
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.RMQ.Interfaces.IRMQPoolAsyncManager" />
    public class RMQPoolAsyncManager : IRMQPoolAsyncManager
    {
        private readonly IRMQPoolAsync _pool;
        private readonly string _connectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RMQPoolAsyncManager"/> class.
        /// </summary>
        /// <param name="pool">The pool.</param>
        /// <param name="connectionName">Name of the connection.</param>
        public RMQPoolAsyncManager(IRMQPoolAsync pool, string connectionName = "connection")
        {
            Guard.AgainstNull(() => pool);
            Guard.AgainstNullOrEmpty(() => connectionName);

            _pool = pool;
            _connectionName = connectionName;
        }

        #region Senders
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="data">The data.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public async Task SendAsync(string queueName, string data, Action<RMQSenderProperties> action = null)
        {
            using (var container = await _pool.GetAsync<IRMQProducerText>(_connectionName, queueName))
            {
                await container.Instance.SendAsync(data, (properties) =>
                {
                    action?.Invoke((RMQSenderProperties)properties);
                });
            }
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="data">The data.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public async Task SendAsync(string queueName, byte[] data, Action<RMQSenderProperties> action = null)
        {
            using (var container = await _pool.GetAsync<IRMQProducerBytes>(_connectionName, queueName))
            {
                await container.Instance.SendAsync(data, (properties) =>
                {
                    action?.Invoke((RMQSenderProperties)properties);
                });
            }
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="data">The data.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public async Task SendAsync(string queueName, object data, Action<RMQSenderProperties> action = null)
        {
            await SendAsync(queueName, JsonConvert.SerializeObject(data), action);
        }

        #endregion Senders

        #region reader
        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="onAsyncMessage">The on asynchronous message.</param>
        /// <param name="onAsyncException">The on asynchronous exception.</param>
        /// <param name="msecTimeout">The msec timeout.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        public async Task ReadAsync(string queueName, Func<IMessageReceiveResult<string>, Task> onAsyncMessage, Func<Exception, Task<bool>> onAsyncException = null, int msecTimeout = Timeout.Infinite, int delay = 2500)
        {           
            using (var container = await _pool.GetAsync<IRMQConsumerText>(_connectionName, queueName))
            {
                var result = (RMQReceiverResultText)null;
                var reader = container.Instance;

                reader.Mode = ConsumerMode.OnNoMessage_ReturnNull;

                try
                {
                    result = reader.Receive(msecTimeout);
                    if (result != null)
                    {
                        await onAsyncMessage(result);
                        result.Acknowledge();
                    }
                }
                catch (MessageException ex)
                {
                    switch (ex.ExceptionCode)
                    {
                        case MessageExceptionCode.ExclusiveLock:
                            await Task.Delay(delay);
                            break;

                        case MessageExceptionCode.LostConnection:
                            await Task.Delay(delay);
                            reader.ClearCacheBuffer();
                            throw;

                        default:
                            reader.ClearCacheBuffer();
                            throw;
                    }
                }
                catch (Exception ex)
                {
                    var handled = false;
                    if(onAsyncException != null)
                    {
                        handled = await onAsyncException(ex);   
                    }

                    if (handled == false)
                    {
                        result?.Reject();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="onAsyncMessage">The on asynchronous message.</param>
        /// <param name="onAsyncException">The on asynchronous exception.</param>
        /// <param name="msecTimeout">The msec timeout.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        public async Task ReadAsync(string queueName, Func<IMessageReceiveResult<byte[]>, Task> onAsyncMessage, Func<Exception, Task<bool>> onAsyncException = null, int msecTimeout = Timeout.Infinite, int delay = 2500)
        {
            using (var container = await _pool.GetAsync<IRMQConsumerBytes>(_connectionName, queueName))
            {
                var result = (RMQReceiverResultByte)null;
                var reader = container.Instance;

                reader.Mode = ConsumerMode.OnNoMessage_ReturnNull;

                try
                {
                    result = reader.Receive(msecTimeout);
                    if (result != null)
                    {
                        await onAsyncMessage(result);
                        result.Acknowledge();
                    }
                }
                catch (MessageException ex)
                {
                    switch (ex.ExceptionCode)
                    {
                        case MessageExceptionCode.ExclusiveLock:
                            await Task.Delay(delay);
                            break;

                        case MessageExceptionCode.LostConnection:
                            await Task.Delay(delay);
                            reader.ClearCacheBuffer();
                            throw;

                        default:
                            reader.ClearCacheBuffer();
                            throw;
                    }
                }
                catch (Exception ex)
                {
                    var handled = false;
                    if (onAsyncException != null)
                    {
                        handled = await onAsyncException(ex);
                    }

                    if (handled == false)
                    {
                        result?.Reject();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}
