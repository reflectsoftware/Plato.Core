// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.Autofac
{
    public interface IDependencyFactory
    {
        T Resolve<T>(string name, params ResolveParameter[] parameters);
        T Resolve<T>(params ResolveParameter[] parameters);
        T Resolve<T>(string name);
        T Resolve<T>();
    }
}
