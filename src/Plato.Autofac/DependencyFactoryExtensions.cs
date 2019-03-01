// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

namespace Plato.Autofac
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyFactoryExtensions
    {
        /// <summary>
        /// Creates the specified dependency factory.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <returns></returns>
        public static TOut Create<TOut>(this IDependencyFactory dependencyFactory)
        {
            return dependencyFactory.Resolve<TOut>();
        }

        /// <summary>
        /// Creates the specified in1.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <param name="t1">The t1.</param>
        /// <returns></returns>
        public static TOut Create<TOut, T1>(this IDependencyFactory dependencyFactory, object t1)
        {
            return dependencyFactory.Resolve<TOut>(new ResolveParameter<T1>(t1));
        }

        /// <summary>
        /// Creates the specified t1.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns></returns>
        public static TOut Create<TOut, T1, T2>(this IDependencyFactory dependencyFactory, object t1, object t2)
        {
            return dependencyFactory.Resolve<TOut>(new ResolveParameter<T1>(t1), new ResolveParameter<T2>(t2));
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static TOut Create<TOut>(this IDependencyFactory dependencyFactory, string name)
        {
            return dependencyFactory.Resolve<TOut>(name);
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="t1">The t1.</param>
        /// <returns></returns>
        public static TOut Create<TOut, T1>(this IDependencyFactory dependencyFactory, string name, object t1)
        {
            return dependencyFactory.Resolve<TOut>(name, new ResolveParameter<T1>(t1));
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns></returns>
        public static TOut Create<TOut, T1, T2>(this IDependencyFactory dependencyFactory, string name, object t1, object t2)
        {
            return dependencyFactory.Resolve<TOut>(name, new ResolveParameter<T1>(t1), new ResolveParameter<T2>(t2));
        }
    }
}
