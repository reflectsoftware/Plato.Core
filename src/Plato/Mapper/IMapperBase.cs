using System.Threading.Tasks;

namespace Plato.Mapper
{
    public interface IMapperBase
    {
        TTarget Map<TSource, TTarget>(TSource source, TTarget target, params object[] args)
            where TSource : class
            where TTarget : class;

        TTarget Map<TSource, TTarget>(TSource source, params object[] args)
            where TSource : class
            where TTarget : class;

        Task<TTarget> MapAsync<TSource, TTarget>(TSource source, TTarget target, params object[] args)
            where TSource : class
            where TTarget : class;

        Task<TTarget> MapAsync<TSource, TTarget>(TSource source, params object[] args)
            where TSource : class
            where TTarget : class;
    }
}
