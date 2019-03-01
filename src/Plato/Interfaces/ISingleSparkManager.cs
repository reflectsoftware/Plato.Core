// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.Interfaces
{
    public interface ISingleSparkManager
    {
        void Ignite<TProcessor>() where TProcessor : ISingleSparkIgniteProcessor;
    }
}
