using System;

namespace Pipline.Core.Interfaces
{
    public interface IPipelineBuilderStep<TIn, TOut>
    {
        IPipelineBuilderStep<TIn, TNewStepOut> AddStep<TNewStepOut> (Func<TOut, TNewStepOut> stepFunc);
        IPipeline<TIn, TOut> CreatePipline ();
    }
}
