// Plato.Core
// Copyright (c) 2020 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.IO;
using MessagePack;

namespace Plato.Serializers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Plato.Serializers.ObjectSerializerBase"/>
    public class ObjectMessagePackSerializer : ObjectSerializerBase
    {
        /// <summary>
        /// Serializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="obj">The object.</param>
        public override void Serialize(Stream stream, object obj)
        {
            MessagePackSerializer.Serialize(obj.GetType(),stream, obj);
        }

        /// <summary>
        /// Deserializes the specified b object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bObj">The b object.</param>
        /// <returns></returns>
        public override T Deserialize<T>(byte[] bObj)
        {
            using (var ms = new MemoryStream(bObj))
            {
                return MessagePackSerializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bObj">The b object.</param>
        /// <returns></returns>
        public override object Deserialize(Type type, byte[] bObj)
        {
            using (var ms = new MemoryStream(bObj))
            {
                return MessagePackSerializer.Deserialize(type,ms);
            }
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public override T Deserialize<T>(Stream stream)
        {
            return MessagePackSerializer.Deserialize<T>(stream);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public override object Deserialize(Type type, Stream stream)
        {
            return MessagePackSerializer.Deserialize(type, stream);
        }
    }
}
