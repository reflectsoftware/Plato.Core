// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plato.Interfaces;

namespace Plato.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionManagerServiceBuilder
    {
        /// <summary>
        /// Adds the exception manager.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddExceptionManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ExceptionManagerSettings>((settings) =>
            {
                configuration.GetSection(nameof(ExceptionManagerSettings)).Bind(settings);
            });

            services.AddSingleton<IExceptionManager, ExceptionManager>();

            return services;
        }
    }
}
