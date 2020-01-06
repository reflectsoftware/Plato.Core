// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Threading.Tasks;

namespace Plato.Cache.Interfaces
{
    public interface ICacheKeyManager
    {
        ICache Cache { get; }
        Task<string> GenerateAsync<TData>(CacheKey<TData> cacheKey, TData data);
        Task ClearAsync<TData>(TData data);
    }
}