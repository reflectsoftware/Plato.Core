// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Plato.DomainEvents
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventActivity<T>
    {
        public Dictionary<string, object> Properties { get; set; }
        public string Category { get; set; }
        public T Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventActivity{T}"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="data">The data.</param>
        public EventActivity(string category, T data)
        {
            Category = category;
            Data = data;
            Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TData GetProperty<TData>(string key)
        {
            var result = default(TData);
            if (Properties.ContainsKey(key))
            {
                result = (TData)Properties[key];
            }

            return result;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        public void SetProperty(string key, object data)
        {
            Properties[key] = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns></returns>
        public TData GetData<TData>()
        {
            return JToken.FromObject(Data).ToObject<TData>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EventActivity : EventActivity<object>
    {
        public EventActivity(string category, object data) : base(category, data)
        {
        }
    }
}
