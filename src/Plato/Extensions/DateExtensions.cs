// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;

namespace Plato.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateExtensions
    {
        /// <summary>
        /// Gets the time in UTC format.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string GetTimeInUTCFormat(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss+0000");
        }

        /// <summary>
        /// Gets the current time in UTC format.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTimeInUTCFormat()
        {
            return GetTimeInUTCFormat(DateTime.Now.ToUniversalTime());
        }
    }
}
