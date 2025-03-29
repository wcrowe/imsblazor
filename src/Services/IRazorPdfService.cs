namespace imsblazor.Services;
public interface IRazorPdfService
{
    Task<byte[]> GenerateFromTemplateAsync<T>(string razorViewPath, T model);
}
