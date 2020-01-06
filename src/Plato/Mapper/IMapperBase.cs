// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Threading.Tasks;

namespace Plato.Mapper
{
    public interface IMapperBase
    {
        TTarget Map<TSource, TTarget>(TSource source, TTarget target, params object[] args)
            where TSource : class
            where TTarget : class;

        TTarget Map<TSource, TTarget>(TSource source, params object[] args)
            where TSource : class
            where TTarget : class;

        Task<TTarget> MapAsync<TSource, TTarget>(TSource source, TTarget target, params object[] args)
            where TSource : class
            where TTarget : class;

        Task<TTarget> MapAsync<TSource, TTarget>(TSource source, params object[] args)
            where TSource : class
            where TTarget : class;
    }
}
