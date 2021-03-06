﻿// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Threading.Tasks;

namespace Plato.Cache.Interfaces
{
    public interface ICacheKeyLock : IDisposable
    {
        bool Disposed { get; }
        bool Locked { get; }

        void Unlock();
        Task UnlockAsync();
    }
}
