// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Threading.Tasks;

namespace Plato.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    public interface IMapper<T1, T2>
    {
        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="args">The arguments.</param>
        void Map(T1 source, T2 target, params object[] args);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    public interface IMapperAsync<T1, T2>
    {
        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        Task MapAsync(T1 source, T2 target, params object[] args);
    }
}