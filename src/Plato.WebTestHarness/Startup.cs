using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plato.Autofac;
using Plato.Messaging.RMQ;
using Plato.Messaging.RMQ.Builder;
using Plato.WebTestHarness.RMQConsumers;
using System;

namespace Plato.WebTestHarness
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }                

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var configManager = new RMQConfigurationManager();
            var connectionSettings = configManager.GetConnectionSettings("connection");
            var exchangeSettings = configManager.GetExchangeSettings("my_rmq_test_exchange");
            var queueSettings = configManager.GetQueueSettings("my_rmq_test");

            //services.AddRMQBoundConsumer<TestBoundConsumerText>(options =>
            //{
            //    options.ConnectionSettings = connectionSettings;
            //    options.QueueSettings = queueSettings;
            //    options.OnException = (ex) => Console.WriteLine(ex);
            //});

            services.AddRMQBoundSubscriber<TestBoundConsumerText>(options =>
            {
                options.ConnectionSettings = connectionSettings;
                options.ExchangeSettings = exchangeSettings;
                options.QueueSettings = queueSettings;                
                options.OnException = (ex) => Console.WriteLine(ex);
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services.AddDependencyInjections();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRMQBoundConsumers();

            app.UseMvc();
        }
    }
}
