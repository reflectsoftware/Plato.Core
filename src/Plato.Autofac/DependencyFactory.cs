// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Autofac;
using System.Linq;

namespace Plato.Autofac
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Scs.Lms.ActivityWatcher.Core.Interfaces.IDependencyFactory" />
    public class DependencyFactory : IDependencyFactory
    {        
        private readonly IComponentContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyFactory" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DependencyFactory(IComponentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T Resolve<T>(string name, params ResolveParameter[] parameters)
        {
            var typedParameters = parameters.Select(param => new TypedParameter(param.ParameterType, param.Parameter));
            return _context.ResolveNamed<T>(name, typedParameters);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T Resolve<T>(params ResolveParameter[] parameters)
        {
            var typedParameters = parameters.Select(param => new TypedParameter(param.ParameterType, param.Parameter));
            return _context.Resolve<T>(typedParameters);
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public T Resolve<T>(string name)
        {
            return _context.ResolveNamed<T>(name);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {            
            return _context.Resolve<T>();
        }
    }
}
