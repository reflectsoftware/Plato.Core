// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.DomainEvents
{
    public interface IDomainEventManager
    {        
        IDomainEventScope Scope();
    }
}