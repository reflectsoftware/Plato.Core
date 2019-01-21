// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.Configuration.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigValueIntercept
    {
        /// <summary>
        /// Adds the variables.
        /// </summary>
        /// <param name="configContainer">The configuration container.</param>
        /// <param name="bForceRefresh">if set to <c>true</c> [b force refresh].</param>
        void AddVariables(IConfigContainer configContainer, bool bForceRefresh = true);

        /// <summary>
        /// Removes the variables.
        /// </summary>
        /// <param name="configContainer">The configuration container.</param>
        /// <param name="bForceRefresh">if set to <c>true</c> [b force refresh].</param>
        void RemoveVariables(IConfigContainer configContainer, bool bForceRefresh = true);

        /// <summary>
        /// Values the intercept.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string ValueIntercept(string value);
    }
}
