using mj.connect;
using mj.worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IDataBases, DataBases>();
        services.AddScoped<IScheduler, Scheduler>();
    })
    .Build();

await host.RunAsync();
