using NotificationService.Interfaces;

namespace NotificationService.Services
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly IWebHostEnvironment _env; // Inject IWebHostEnvironment
        private readonly ILogger<TemplateProvider> _logger;

        public TemplateProvider(IWebHostEnvironment env, ILogger<TemplateProvider> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task<string> GetTemplateAsync(string messageType)
        {
            string templatePath = Path.Combine(_env.ContentRootPath, "Templates", $"{messageType}.html"); // "Templates" folder

            if (File.Exists(templatePath))
            {
                return await File.ReadAllTextAsync(templatePath);
            }

            _logger.LogError($"Template file not found: {templatePath}");
            return null;
        }
    }
}
