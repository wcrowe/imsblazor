
public class NotificationService
{
    public Task SendEmailAsync(string to, string subject, string body) => Task.CompletedTask;
    public void SendSms(string to, string message) {}
}
