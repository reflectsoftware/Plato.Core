// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Reflection;
using System.Text;

namespace Plato.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Clones the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static T CloneObject<T>(this T obj) where T : class
        {
            var inst = obj?.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
            return (T)inst?.Invoke(obj, null);
        }

        /// <summary>
        /// Determines whether [is standard type] [the specified typ].
        /// </summary>
        /// <param name="typ">The typ.</param>
        /// <returns>
        ///   <c>true</c> if [is standard type] [the specified typ]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStandardType(this Type typ)
        {
            if (typ == typeof(bool))
            {
                return true;
            }
            if (typ == typeof(byte))
            {
                return true;
            }
            if (typ == typeof(sbyte))
            {
                return true;
            }
            if (typ == typeof(char))
            {
                return true;
            }
            if (typ == typeof(decimal))
            {
                return true;
            }
            if (typ == typeof(double))
            {
                return true;
            }
            if (typ == typeof(float))
            {
                return true;
            }
            if (typ == typeof(int))
            {
                return true;
            }
            if (typ == typeof(uint))
            {
                return true;
            }
            if (typ == typeof(long))
            {
                return true;
            }
            if (typ == typeof(ulong))
            {
                return true;
            }
            if (typ == typeof(short))
            {
                return true;
            }
            if (typ == typeof(ushort))
            {
                return true;
            }
            if (typ == typeof(string))
            {
                return true;
            }
            if (typ == typeof(StringBuilder))
            {
                return true;
            }
            if (typ == typeof(DateTime))
            {
                return true;
            }
            if (typ.IsEnum)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is standard type] [the specified object].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if [is standard type] [the specified object]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStandardType(this object obj)
        {
            return IsStandardType(obj.GetType());
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void DisposeObject(this object obj)
        {
            var dObj = obj as IDisposable;
            dObj?.Dispose();
        }
    }
}
