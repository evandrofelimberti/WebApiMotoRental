using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MotoRentalService;
using WebApiMotoRental.Interfaces;
using WebApiMotoRental.Services;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {

        services.AddDbContextFactory<DataContextService>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseLowerCaseNamingConvention();
        });
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddSingleton<VeiculoRepositoryImpl, VeiculoRepository>();
        services.AddHostedService<VeiculoConsumer>();
    })
    .Build();

await host.RunAsync();
