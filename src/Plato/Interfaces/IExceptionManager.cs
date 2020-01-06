// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Plato.Interfaces
{
    public interface IExceptionManager
    {
        void Handle(Exception ex, NameValueCollection additionalInfo = null, bool ignoreTracking = false);
        Task HandleAsync(Exception ex, NameValueCollection additionalInfo = null, bool ignoreTracking = false);
    }
}
