﻿// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Interfaces;

namespace Plato.Messaging.AMQ.Interfaces
{
    public interface IAMQSenderText: IAMQSender, IMessageSender<string>
    {
    }
}
