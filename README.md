## Usage
1. **Register Message Broker and Event Handlers:**
   Declare the Message Broker and register events with their event handlers through Dependency Injection. Follow the steps below:

   1.1. **Declare Message Broker through Dependency Injection:**
        Add the following line of code in your application startup:

        ```csharp
        // In your Startup.cs file or equivalent
        public void ConfigureServices(IServiceCollection services)
        {
            // Other service configurations

            // Add Message Broker to services
            `builder.Services.AddMessageBroker();
        }
        ```

   1.2. **Register Events and Event Handlers:**
        Register events and their event handlers through Dependency Injection. Use the following example:

        ```csharp
        // In your Startup.cs file or equivalent
        public void ConfigureServices(IServiceCollection services)
        {
            // Other service configurations

            // Register events and event handlers
            builder.Services.AddTransient<DomainEventMessageHandler<TestingEvent>, TestingEventEventHandler>();
            builder.Services.AddTransient<DomainEventMessageHandler<TestingEvent2>, TestingEventEventHandler2>();
        }
        ```

2. **Start Consumers:**
   Start the message consumers in your application. Depending on your application type, you can do this in different ways. For example, using Dependency Injection with `IMessageBroker`:

    2.1. **Example for Hosting Consumers as a Background Service:**
        Create a hosted service that starts and stops the consumers. For instance:

        ```csharp
        // In your hosted service class or equivalent
        public class ConsumeHostedService : BackgroundService, IHostedService, IDisposable
        {
            private readonly IMessageBroker messageBroker;

            public ConsumeHostedService(IMessageBroker messageBroker)
            {
                this.messageBroker = messageBroker;
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                return Task.CompletedTask;
            }

            public override Task StartAsync(CancellationToken cancellationToken)
            {
                messageBroker.StartAllConsumersAsync().Wait(CancellationToken.None);
                return base.StartAsync(cancellationToken);
            }

            public override Task StopAsync(CancellationToken cancellationToken)
            {
                messageBroker.StopAllConsumersAsync().Wait(CancellationToken.None);
                return base.StopAsync(cancellationToken);
            }

            public override void Dispose()
            {
                messageBroker.StopAllConsumersAsync().Wait(CancellationToken.None);
                base.Dispose();
            }
        }
        ```