// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Redis.Interfaces;
using StackExchange.Redis;

namespace Plato.Redis.Interfaces
{
    public interface IRedisCollectionFactory
    {
        IRedisDictionary<TKey, TValue> CreateDictionary<TKey, TValue>(IDatabase redisDb, RedisKey redisKey, IRedisSerializer valueSerializer = null);
        IRedisList<TValue> CreateList<TValue>(IDatabase redisDb, RedisKey redisKey, IRedisSerializer valueSerializer = null);
        IRedisQueue<TValue> CreateQueue<TValue>(IDatabase redisDb, RedisKey redisKey, IRedisSerializer valueSerializer = null);
        IRedisStack<TValue> CreateStack<TValue>(IDatabase redisDb, RedisKey redisKey, IRedisSerializer valueSerializer = null);
    }
}