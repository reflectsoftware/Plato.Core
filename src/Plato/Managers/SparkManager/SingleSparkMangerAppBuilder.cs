// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Plato.Interfaces;

namespace Plato.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public static class SingleSparkMangerAppBuilder
    {
        /// <summary>
        /// Uses the single spark manager.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSingleSparkManager<TProcess>(this IApplicationBuilder app) where TProcess : ISingleSparkIgniteProcessor
        {
            var sparkManager = app.ApplicationServices.GetService<ISingleSparkManager>();
            sparkManager.Ignite<TProcess>();

            return app;
        }
    }
}
