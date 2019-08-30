using System;
using System.Threading.Tasks;
using Disruptor.Dsl;
using Pipline.Core.Interfaces;

namespace Pipline.Core.Implementations
{
    public class RequestFormPipeline<TIn, TOut> : IPipeline<TIn, TOut>
    {
        private readonly Disruptor<RequestEvent> _disruptor;

        public RequestFormPipeline(Disruptor<RequestEvent> disruptor)
        {
            if (disruptor is null) throw new ArgumentNullException("Event handler can not be null");

            _disruptor = disruptor;            
            _disruptor.Start();
        }
        public Task<TOut> Execute(TIn data)
        {
            // RunContinuationsAsynchronously to prevent continuation from "stealing" the releaser thread
            var tcs = new TaskCompletionSource<TOut>(TaskCreationOptions.RunContinuationsAsynchronously);

            var sequence = _disruptor.RingBuffer.Next();
            var disruptorEvent = _disruptor[sequence];
            disruptorEvent.Write(data);
            disruptorEvent.TaskCompletionSource = tcs;
            _disruptor.RingBuffer.Publish(sequence);

            return tcs.Task;
        }

        public void Dispose() => _disruptor?.Shutdown();
    }
}
