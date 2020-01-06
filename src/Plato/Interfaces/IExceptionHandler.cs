// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Collections.Specialized;

namespace Plato.Interfaces
{
    public interface IExceptionHandler
    {
        string Name { get; }
        void Handle(Exception ex, NameValueCollection additionalInfo = null);
    }
}
