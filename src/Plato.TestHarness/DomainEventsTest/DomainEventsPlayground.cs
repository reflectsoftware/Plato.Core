using global::SimpleInjector;
using MediatR;
using Plato.DomainEvents;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plato.TestHarness.DomainEventsTest
{
    #region declarations
    public class EventOne : DomainEvent
    {
        public string Label { get; }

        public EventOne(string label)
        {
            Label = $"One: {label}";
        }        
    }

    public class EventTwo : DomainEvent
    {
        public string Label { get; }

        public EventTwo(string label)
        {
            Label = $"Two: {label}";
        }
    }


    public class EventThree: DomainEvent
    {
        public string Label { get; }

        public EventThree(string label)
        {
            Label = $"Three: {label}";
        }
    }
    #endregion declarations

    public class DomainEventHandler : INotificationHandler<DomainEventNotification>
    {
        public Task Handle(DomainEventNotification notification, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in notification.Events)
            {            
                if(domainEvent is EventOne)
                {
                    Console.WriteLine((domainEvent as EventOne).Label);
                }

                if (domainEvent is EventTwo)
                {
                    Console.WriteLine((domainEvent as EventTwo).Label);
                }

                if (domainEvent is EventThree)
                {
                    Console.WriteLine((domainEvent as EventThree).Label);
                }
            }

            return Task.CompletedTask;
        }
    }


    public static class DomainEventsPlayground
    {
        static public async Task RunAsync()
        {
            using (var container = new Container())
            {
                var mediator = BuildMediator(container);

                IDomainEventManager manager = new DomainEventManager(mediator);
                using (var s1 = manager.Scope())
                {
                    await s1.AddAsync(new EventOne("Ross"));
                    await s1.AddAsync(new EventOne("Tammy"));

                    using (var s2 = manager.Scope())
                    {
                        using (var s3 = manager.Scope())
                        {
                            await s3.AddAsync(new EventThree("Ross"));
                            await s3.AddAsync(new EventThree("Tammy"));
                            await s3.BroadcastAsync();
                        }

                        await s2.AddAsync(new EventTwo("Ross"));
                        await s2.AddAsync(new EventTwo("Tammy"));
                        await s2.BroadcastAsync();
                    }

                    await s1.BroadcastAsync();
                }
            }
        }

        #region mediator builder
        private static IMediator BuildMediator(Container container)
        {            
            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var notificationHandlerTypes = container.GetTypesToRegister(typeof(INotificationHandler<>), new[] { Assembly.GetEntryAssembly() }, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), Assembly.GetEntryAssembly());
            container.Collection.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
            container.Verify();

            var mediator = container.GetInstance<IMediator>();

            return mediator;
        }
        #endregion mediator builder
    }
}
