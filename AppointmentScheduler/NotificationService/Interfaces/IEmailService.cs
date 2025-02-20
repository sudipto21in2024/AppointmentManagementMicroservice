namespace NotificationService.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, Dictionary<string, string> mergeData);
        Task SendScheduledEmailsAsync(); // For daily emails
    }
}
