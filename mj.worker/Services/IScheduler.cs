public interface IScheduler
{
    Task RunScheduleAsync(CancellationToken stoppingToken);
}