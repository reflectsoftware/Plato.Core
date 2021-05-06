// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using Microsoft.AspNetCore.Http;
using Plato.ExceptionIntercepts.ExceptionInterceptHandler.Interfaces;

namespace Plato.ExceptionIntercepts.ExceptionInterceptHandler
{
    /// <summary>
    /// The exception context that holds the HttpContext and the actual
    /// unhandled exception that occurred in
    /// </summary>
    /// <seealso cref="Plato.ExceptionIntercepts.ExceptionInterceptHandler.Interfaces.IExceptionInterceptContext" />    
    public class ExceptionInterceptContext : IExceptionInterceptContext
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public HttpContext Context { get; set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; set; }


        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void SetException(Exception ex)
        {
            Exception = ex ?? Exception;
        }
    }
}
