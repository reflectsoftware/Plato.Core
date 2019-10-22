using Plato.Cache.Interfaces;
using System;

namespace Plato.Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <seealso cref="Plato.Cache.Interfaces.ICacheKey" />
    public class CacheKey<TData> : ICacheKey
    {
        public string Pattern { get; protected set; }
        public Type DataType { get; protected set; }

        private readonly Func<TData, string, string> _onKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey" /> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="onKey">The on key.</param>
        public CacheKey(string pattern, Func<TData, string, string> onKey)
        {
            Pattern = pattern;
            DataType = typeof(TData);
            _onKey = onKey;
        }

        /// <summary>
        /// Generates the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string Generate(TData data)
        {
            return _onKey(data, Pattern);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Pattern.GetHashCode() + DataType.GetHashCode();
        }
    }
}
