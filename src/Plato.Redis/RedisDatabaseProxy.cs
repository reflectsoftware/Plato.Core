// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using StackExchange.Redis;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Plato.Redis
{
    /// <summary>
    /// https://www.c-sharpcorner.com/article/aspect-oriented-programming-in-c-sharp-using-dispatchproxy/
    /// </summary>
    /// <seealso cref="System.Reflection.DispatchProxy" />
    public class RedisDatabaseProxy : DispatchProxy
    {
        private IDatabase _database;
        private RedisDatabaseProxyConfiguration _configuration;

        /// <summary>
        /// Creates the specified database.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static IDatabase Create(IDatabase database, RedisDatabaseProxyConfiguration config = null)
        {
            var proxy = Create<IDatabase, RedisDatabaseProxy>();

            ((RedisDatabaseProxy)proxy)._database = database;
            ((RedisDatabaseProxy)proxy)._configuration = config ?? new RedisDatabaseProxyConfiguration();

            return proxy;
        }

        /// <summary>
        /// Considers the retry.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        private bool ConsiderRetry(Exception ex)
        {
            var testException = (ex is AggregateException) ? ex.InnerException : ex;
            return (testException is RedisServerException) || (testException is RedisConnectionException);
        }

        /// <summary>
        /// Invokes the specified method information.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        protected override object Invoke(MethodInfo methodInfo, object[] args)
        {
            var retry = _configuration.Retry;
            while (true)
            {
                try
                {
                    var result = methodInfo.Invoke(_database, args);
                    var exception = (Exception)null;

                    if (result is Task resultTask)
                    {
                        resultTask.ContinueWith(task =>
                        {
                            if (task.Exception != null)
                            {
                                exception = task.Exception;
                            }
                            else
                            {
                                object taskResult = null;

                                if (task.GetType().GetTypeInfo().IsGenericType
                                && task.GetType().GetGenericTypeDefinition() == typeof(Task<>))
                                {
                                    var property = task.GetType()
                                        .GetTypeInfo()
                                        .GetProperties()
                                        .FirstOrDefault(p => p.Name == "Result");

                                    if (property != null)
                                    {
                                        taskResult = property.GetValue(task);
                                    }
                                }
                            }
                        });
                    }

                    if (exception != null)
                    {
                        throw new TargetInvocationException(exception);
                    }

                    return result;
                }
                catch (TargetInvocationException ex)
                {
                    retry--;
                    if (retry >= 0 && ConsiderRetry(ex.InnerException))
                    {
                        Task.Delay(_configuration.RetryWait).Wait();
                        continue;
                    }

                    throw ex.InnerException ?? ex;
                }
            }
        }
    }
}
