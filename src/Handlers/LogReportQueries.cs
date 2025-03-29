
using MediatR;
using System.Data;
using Dapper;
using imsblazor.Services;
using imsblazor.Fluxor;
using System.Text;
namespace imsblazor.Handlers;
public record GenerateWeeklyReportQuery : IRequest<byte[]>;
public class GenerateWeeklyReportHandler : IRequestHandler<GenerateWeeklyReportQuery, byte[]>
{
    private readonly IDbConnection _db;
    private readonly IRazorPdfService _pdf;

    public GenerateWeeklyReportHandler(IDbConnection db, IRazorPdfService pdf)
    {
        _db = db;
        _pdf = pdf;
    }

    public async Task<byte[]> Handle(GenerateWeeklyReportQuery request, CancellationToken cancellationToken)
    {
        var model = (await _db.QueryAsync<LogSummaryExportDto>("sp_GetWeeklyLogSummary", commandType: CommandType.StoredProcedure)).ToList();
        return await _pdf.GenerateFromTemplateAsync("Reports/WeeklyLogReport", model);
    }
}

public record ExportAuditLogQuery : IRequest<byte[]>;
public class ExportAuditLogHandler : IRequestHandler<ExportAuditLogQuery, byte[]>
{
    private readonly IDbConnection _db;

    public ExportAuditLogHandler(IDbConnection db)
    {
        _db = db;
    }

    public async Task<byte[]> Handle(ExportAuditLogQuery request, CancellationToken cancellationToken)
    {
        var logs = (await _db.QueryAsync<AuditLogExportDto>("sp_GetAuditLogsForExport", commandType: CommandType.StoredProcedure)).ToList();
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        using var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);
        csv.WriteRecords(logs);
        writer.Flush();
        stream.Position = 0;
        return stream.ToArray();
    }
}
