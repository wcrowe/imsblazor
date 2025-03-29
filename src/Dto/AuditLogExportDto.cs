
public class AuditLogExportDto
{
    public DateTime TimeStamp { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
}
