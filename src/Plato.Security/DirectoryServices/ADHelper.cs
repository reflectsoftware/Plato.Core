// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.Security.DirectoryServices
{
    /// <summary>
    ///
    /// </summary>
    public static class ADHelper
    {
        /// <summary>
        /// Gets the distinguish name property value.
        /// </summary>
        /// <param name="distinguishName">Name of the distinguish.</param>
        /// <param name="property">The property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetDistinguishNamePropertyValue(string distinguishName, string property, string defaultValue)
        {
            property = property.Trim().ToLower();

            var parts = distinguishName.Split(',');
            foreach (var part in parts)
            {
                var subparts = part.Split('=');
                if (subparts[0].ToLower().Trim() == property)
                {
                    if (subparts.Length == 2)
                    {
                        return subparts[1];
                    }

                    return defaultValue;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the distinguish name property value.
        /// </summary>
        /// <param name="distinguishName">Name of the distinguish.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static string GetDistinguishNamePropertyValue(string distinguishName, string property)
        {
            return GetDistinguishNamePropertyValue(distinguishName, property, string.Empty);
        }
    }
}
