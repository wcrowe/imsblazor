
public class LogRetentionService : BackgroundService
{
    private readonly ILogger<LogRetentionService> _logger;
    private readonly IDbConnection _db;
    private readonly IConfiguration _config;

    public LogRetentionService(ILogger<LogRetentionService> logger, IDbConnection db, IConfiguration config)
    {
        _logger = logger;
        _db = db;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var days = _config.GetValue<int>("LogRetention:Days");
            await _db.ExecuteAsync("DELETE FROM Logs WHERE TimeStamp < DATEADD(DAY, -@Days, SYSDATETIMEOFFSET())", new { Days = days });
            _logger.LogInformation("Log retention cleanup complete.");
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
