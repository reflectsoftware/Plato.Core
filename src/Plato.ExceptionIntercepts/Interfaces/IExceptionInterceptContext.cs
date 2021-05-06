// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using Microsoft.AspNetCore.Http;

namespace Plato.ExceptionIntercepts.ExceptionInterceptHandler.Interfaces
{
    public interface IExceptionInterceptContext
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        HttpContext Context { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        Exception Exception { get; }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void SetException(Exception ex);
    }
}
