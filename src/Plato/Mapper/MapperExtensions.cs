// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Threading.Tasks;

namespace Plato.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="mapper">The mapper.</param>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(this IMapper<TSource, TTarget> mapper, TSource source, TTarget target)
            where TSource : class
            where TTarget : class            
        {
            mapper.Map(source, target);            
            return target;
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="mapper">The mapper.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(this IMapper<TSource, TTarget> mapper, TSource source)
            where TSource : class
            where TTarget : class            
        {
            var target = Activator.CreateInstance<TTarget>();
            mapper.Map(source, target);

            return target;
        }

        /// <summary>
        /// Maps the specified mapper.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(this TSource source, IMapper<TSource, TTarget> mapper, TTarget target)
            where TSource : class
            where TTarget : class            
        {
            mapper.Map(source, target);

            return target;
        }

        /// <summary>
        /// Maps the specified mapper.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(this TSource source, IMapper<TSource, TTarget> mapper)
            where TSource : class
            where TTarget : class            
        {
            var target = Activator.CreateInstance<TTarget>();
            mapper.Map(source, target);

            return target;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public static class MapperAsyncExtensions
    {
        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="mapper">The mapper.</param>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static async Task<TTarget> MapAsync<TSource, TTarget>(this IMapperAsync<TSource, TTarget> mapper, TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            await mapper.MapAsync(source, target);
            return target;
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="mapper">The mapper.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static async Task<TTarget> MapAsync<TSource, TTarget>(this IMapperAsync<TSource, TTarget> mapper, TSource source)
            where TSource : class
            where TTarget : class
        {
            var target = Activator.CreateInstance<TTarget>();
            await mapper.MapAsync(source, target);

            return target;
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static async Task<TTarget> MapAsync<TSource, TTarget>(this TSource source, IMapperAsync<TSource, TTarget> mapper, TTarget target)
            where TSource : class
            where TTarget : class
        {
            await mapper.MapAsync(source, target);

            return target;
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns></returns>
        public static async Task<TTarget> MapAsync<TSource, TTarget>(this TSource source, IMapperAsync<TSource, TTarget> mapper)
            where TSource : class
            where TTarget : class
        {
            var target = Activator.CreateInstance<TTarget>();
            await mapper.MapAsync(source, target);

            return target;
        }
    }
}
