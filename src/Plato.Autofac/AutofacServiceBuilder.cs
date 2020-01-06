// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Plato.Autofac
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutofacServiceBuilder
    {
        /// <summary>
        /// Adds the dependency injections.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="modules">The modules.</param>
        /// <returns></returns>
        public static IServiceProvider AddDependencyInjections(this IServiceCollection services, params Module[] modules)
        {
            var container = (IContainer)null;
            var builder = new ContainerBuilder();

            // required
            builder.Register(ctx => container).SingleInstance();
            builder.Register<IDependencyFactory>(ctx => new DependencyFactory(container)).SingleInstance();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            builder.Populate(services);

            container = builder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
