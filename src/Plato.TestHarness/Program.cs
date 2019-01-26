using Plato.Configuration;

namespace Plato.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test RMQ
            // Test AMQ
            // Test Redis

            // ConfigurationTest.ConfigurationPlayground.RunAsync().GetAwaiter();
            // RedisTest.RedisPlayground.RunAsync().GetAwaiter();     
            // Messaging.RMQPlayground.RunAsync().GetAwaiter();
            Messaging.RMQPlayground.Run();
        }
    }
}
