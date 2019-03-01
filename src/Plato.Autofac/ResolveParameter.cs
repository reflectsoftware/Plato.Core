// Plato.Core
// Copyright (c) 2019 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;

namespace Plato.Autofac
{
    /// <summary>
    /// 
    /// </summary>    
    public class ResolveParameter
    {
        public Type ParameterType { get; private set; }
        public object Parameter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveParameter"/> class.
        /// </summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="parameter">The parameter.</param>
        public ResolveParameter(Type parameterType, object parameter)
        {
            ParameterType = parameterType;
            Parameter = parameter;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResolveParameter<T> : ResolveParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveParameter{T}"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public ResolveParameter(object parameter) : base(typeof(T), parameter)
        {
        }
    }
}
