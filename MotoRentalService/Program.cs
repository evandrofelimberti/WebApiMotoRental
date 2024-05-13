using MotoRentalService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<VeiculoConsumer>();
    })
    .Build();

await host.RunAsync();
