// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Cache.Interfaces;
using Plato.Miscellaneous;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Plato.Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Cache.Interfaces.ICacheKeyManager" />
    public class CacheKeyManager : ICacheKeyManager
    {
        private readonly ConcurrentDictionary<ICacheKey, ICacheKey> _cacheKeys;

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>
        /// The cache.
        /// </value>
        public ICache Cache { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKeyManager"/> class.
        /// </summary>
        public CacheKeyManager(ICache cache)
        {
            Guard.AgainstNull(() => cache);

            _cacheKeys = new ConcurrentDictionary<ICacheKey, ICacheKey>();
            Cache = cache;
        }

        /// <summary>
        /// Generates the specified cache key.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public Task<string> GenerateAsync<TData>(CacheKey<TData> cacheKey, TData data)
        {
            _cacheKeys.TryAdd(cacheKey, cacheKey);

            return Task.FromResult(cacheKey.Generate(data));
        }

        /// <summary>
        /// Clears the specified data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="data">The data.</param>
        public async Task ClearAsync<TData>(TData data)
        {
            var dataType = typeof(TData);

            foreach (var cacheKey in _cacheKeys.Values)
            {
                var castCacheKey = (cacheKey as CacheKey<TData>);
                if (castCacheKey?.DataType == dataType)
                {
                    await Cache.RemoveAsync(castCacheKey.Generate(data));
                }
            }
        }
    }
}
