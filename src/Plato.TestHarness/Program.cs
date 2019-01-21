using System;

namespace Plato.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Test
            // Test Configuration - especially app.config file is read and loaded
            //   Is config section respected - appsettings, plato.settings, etc.

            RedisTest.RedisPlayground.RunAsync().GetAwaiter();
        }
    }
}
