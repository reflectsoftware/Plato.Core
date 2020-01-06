// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Threading.Tasks;

namespace Plato.ExceptionIntercepts.ExceptionInterceptHandler.Interfaces
{
    /// <summary>
    /// IExceptionInterceptHandler.
    /// </summary>
    public interface IExceptionInterceptHandler
    {
        /// <summary>
        /// Handles exception asynchronously.
        /// </summary>
        /// <param name="exceptionContext">The exception context.</param>
        /// <returns></returns>
        Task HandleAsync(IExceptionInterceptContext exceptionContext);
    }
}
