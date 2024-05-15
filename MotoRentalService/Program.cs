using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MotoRentalService;
using WebApiMotoRental.Data;
using WebApiMotoRental.Interfaces;
using WebApiMotoRental.Services;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

        services.AddDbContextPool<DataContext>(options =>
        {
            options.UseNpgsql(connectionString).UseLowerCaseNamingConvention();
        });
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        //services.AddScoped<VeiculoRepositoryImpl, VeiculoRepository>();
        //services.AddSingleton<VeiculoServiceImpl, VeiculoService>();
        services.AddScoped(typeof(VeiculoRepositoryImpl), typeof(VeiculoRepository));
        services.AddHostedService<VeiculoConsumer>();
    })
    .Build();

await host.RunAsync();
