// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Threading.Tasks;

namespace Plato.Interfaces
{
    public interface ISingleSparkManagerLifeCycle
    {
        Task<bool> KeepAliveAsync();
        Task<bool> IsAliveAsync();
        Task TerminateAsync();
        Task<bool> IsTerminatedAsync();
        Task RelaxAsync();
    }
}
