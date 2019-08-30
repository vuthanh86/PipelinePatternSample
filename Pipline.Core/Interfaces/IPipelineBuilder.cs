using System;

namespace Pipline.Core.Interfaces
{
    public interface IPipelineBuilder
    {
        IPipelineBuilderStep<TStepIn, TStepOut> Build<TStepIn, TStepOut> (Func<TStepIn, TStepOut> stepFunc);
    }
}
