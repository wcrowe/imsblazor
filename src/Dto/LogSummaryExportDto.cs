
public class LogSummaryExportDto
{
    public string UserId { get; set; }
    public DateTime LogDate { get; set; }
    public int TotalLogs { get; set; }
    public int ErrorCount { get; set; }
    public int AuditCount { get; set; }
}
