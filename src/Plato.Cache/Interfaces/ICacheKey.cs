using System;

namespace Plato.Cache.Interfaces
{
    public interface ICacheKey
    {
        Type DataType { get; }
    }
}
