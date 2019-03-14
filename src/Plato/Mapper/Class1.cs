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
        /// <returns></returns>
        public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return MapperExtensions.Map((IMapper<TSource, TTarget>)this, source, target);
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TTarget Map<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return MapperExtensions.Map((IMapper<TSource, TTarget>)this, source);
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public Task<TTarget> MapAsync<TSource, TTarget>(TSource source, TTarget target)
                  where TSource : class
                  where TTarget : class
        {
            return MapperAsyncExtensions.MapAsync((IMapperAsync<TSource, TTarget>)this, source, target);
        }

        /// <summary>
        /// Maps the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public Task<TTarget> MapAsync<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return MapperAsyncExtensions.MapAsync((IMapperAsync<TSource, TTarget>)this, source);
        }
    }
}
