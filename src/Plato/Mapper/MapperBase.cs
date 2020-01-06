// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Threading.Tasks;

namespace Plato.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Mapper.IMapperBase" />
    public abstract class MapperBase : IMapperBase
    {
        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public TTarget Map<TSource, TTarget>(TSource source, TTarget target, params object[] args)
            where TSource : class
            where TTarget : class
        {
            var mapper = (IMapper<TSource, TTarget>)this;
            mapper.Map(source, target, args);

            return target;
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public TTarget Map<TSource, TTarget>(TSource source, params object[] args)
            where TSource : class
            where TTarget : class
        {
            var target = Activator.CreateInstance<TTarget>();
            var mapper = (IMapper<TSource, TTarget>)this;
            mapper.Map(source, target, args);

            return target;
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public async Task<TTarget> MapAsync<TSource, TTarget>(TSource source, TTarget target, params object[] args)
            where TSource : class
            where TTarget : class
        {
            var mapper = (IMapperAsync<TSource, TTarget>)this;
            await mapper.MapAsync(source, target, args);
            return target;
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public async Task<TTarget> MapAsync<TSource, TTarget>(TSource source, params object[] args)
            where TSource : class
            where TTarget : class
        {
            var target = Activator.CreateInstance<TTarget>();
            var mapper = (IMapperAsync<TSource, TTarget>)this;            
            await mapper.MapAsync(source, target, args);

            return target;
        }
    }
}
