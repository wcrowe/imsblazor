namespace imsblazor.Services;
using System.Data;
using Dapper;

public class AuditLogger
{
    private readonly IDbConnection _db;
    private readonly IHttpContextAccessor _httpContext;

    public AuditLogger(IDbConnection db, IHttpContextAccessor httpContext)
    {
        _db = db;
        _httpContext = httpContext;
    }

    public async Task LogAsync(string message)
    {
        var user = _httpContext.HttpContext?.User?.Identity?.Name ?? "unknown";
        await _db.ExecuteAsync("sp_LogAudit", new { Message = message, UserId = user }, commandType: CommandType.StoredProcedure);
    }
}
