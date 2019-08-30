using System;
using System.Threading.Tasks;

namespace Pipline.Core.Interfaces
{
    public interface IPipeline<TIn, TOut> : IDisposable
    {
        Task<TOut> Execute (TIn data);
    }
}
