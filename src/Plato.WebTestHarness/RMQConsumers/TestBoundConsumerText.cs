using Plato.Messaging.RMQ;
using Plato.Messaging.RMQ.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plato.WebTestHarness.RMQConsumers
{
    public class TestBoundConsumerText : IRMQBoundConsumerText, IDisposable
    {
        public TestBoundConsumerText()
        {
        }

        public void Dispose()
        {
        }

        public Task OnMessageAsync(RMQReceiverResultText result, CancellationToken cancellationToken)
        {
            Console.WriteLine(result.Data);
            return Task.CompletedTask;
        }
    }
}
