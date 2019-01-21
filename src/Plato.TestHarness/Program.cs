using System;

namespace Plato.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisTest.RedisPlayground.RunAsync().GetAwaiter();
        }
    }
}
