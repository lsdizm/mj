namespace mj.worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider, 
    ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("start");
        await DoWorkAsync(stoppingToken);
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("doWork");
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IScheduler scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScheduler>();
            await scopedProcessingService.RunScheduleAsync(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("stop");
        await base.StopAsync(stoppingToken);
    }
}
