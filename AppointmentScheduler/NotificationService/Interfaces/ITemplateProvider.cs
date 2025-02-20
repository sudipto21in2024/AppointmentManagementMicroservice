namespace NotificationService.Interfaces
{
    public interface ITemplateProvider
    {
        Task<string> GetTemplateAsync(string messageType);
    }
}
