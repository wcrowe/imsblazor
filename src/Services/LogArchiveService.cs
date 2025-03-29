using Dapper;
using System.Data;

namespace imsblazor.Services;
public class LogArchiveService : BackgroundService
{
    private readonly ILogger<LogArchiveService> _logger;
    private readonly IDbConnection _db;
    private readonly IConfiguration _config;

    public LogArchiveService(ILogger<LogArchiveService> logger, IDbConnection db, IConfiguration config)
    {
        _logger = logger;
        _db = db;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var days = _config.GetValue<int>("LogArchive:Days");
            var sql = @"
                INSERT INTO LogArchive (TimeStamp, Level, Message, MessageTemplate, Exception, LogEvent, UserId)
                SELECT TimeStamp, Level, Message, MessageTemplate, Exception, LogEvent, UserId
                FROM Logs
                WHERE TimeStamp < DATEADD(DAY, -@Days, SYSDATETIMEOFFSET());

                DELETE FROM Logs WHERE TimeStamp < DATEADD(DAY, -@Days, SYSDATETIMEOFFSET());";

            await _db.ExecuteAsync(sql, new { Days = days });
            _logger.LogInformation("Log archiving complete.");
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
