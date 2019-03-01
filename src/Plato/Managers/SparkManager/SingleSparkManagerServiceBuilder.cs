// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plato.Interfaces;

namespace Plato.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public static class SingleSparkManagerServiceBuilder
    {
        /// <summary>
        /// Adds the exception manager.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddSingleSparkManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<SingleSparkManagerLifeCycleSettings>((settings) =>
            {
                configuration.GetSection(nameof(SingleSparkManagerLifeCycleSettings)).Bind(settings);
            });

            services.AddSingleton<ISingleSparkManager, SingleSparkManager>();
            services.AddSingleton<ISingleSparkManagerLifeCycle, SingleSparkManagerLifeCycle>();            

            services.AddExceptionManager(configuration);

            return services;
        }
    }
}
