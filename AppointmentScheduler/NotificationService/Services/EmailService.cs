using Microsoft.Extensions.Options;
using MimeKit;
using NotificationService.Interfaces;
using MailKit.Net.Smtp;
using NotificationService.Settings;

namespace NotificationService.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IServiceCatalogService _serviceCatalogService;
        private readonly IUserService _userService;
        private readonly ITemplateProvider _templateProvider;
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<EmailService> _logger; // Add logger

        public EmailService(IOptions<MailSettings> mailSettings, IServiceCatalogService serviceCatalogService, IUserService userService, ITemplateProvider templateProvider, ILogger<EmailService> logger, IAppointmentService appointmentService)
        {
            _mailSettings = mailSettings.Value;
            _serviceCatalogService = serviceCatalogService;
            _userService = userService;
            _templateProvider = templateProvider;
            _appointmentService = appointmentService;
            _logger = logger; // Initialize logger
        }

        public async Task SendEmailAsync(string to, string subject, string messageType, Dictionary<string, string> mergeData)
        {
            try
            {
                string template = await _templateProvider.GetTemplateAsync(messageType);

                if (string.IsNullOrEmpty(template))
                {
                    _logger.LogError($"Template not found for message type: {messageType}");
                    return;
                }

                string mergedBody = MergeTemplate(template, mergeData);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", to)); // Add recipient name if available
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = mergedBody };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.Port, _mailSettings.UseSsl);
                    await client.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    _logger.LogInformation($"Email sent successfully to {to} with subject '{subject}'.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {to}: {ex.Message}");
                // Handle the exception appropriately (e.g., retry, log, throw)
                throw; // Re-throw if you want the calling method to handle it
            }
        }

        private string MergeTemplate(string template, Dictionary<string, string> data)
        {
            foreach (var kvp in data)
            {
                template = template.Replace($"{{{{{kvp.Key}}}}}", kvp.Value); // Use {{{key}}} placeholders
            }
            return template;
        }


        public async Task SendScheduledEmailsAsync()
        {
            // 1. Get appointments scheduled for today (from your data source)
            var appointments = await _appointmentService.GetAppointmentsForToday(); // Implement this method

            foreach (var appointment in appointments)
            {
                // 2. Get user and service provider details
                var service = await _serviceCatalogService.GetServiceByIdAsync(appointment.ServiceId);
                var user = await _userService.GetUserByIdAsync(appointment.CustomerId);

                // 3. Create mail merge data
                var mergeData = new Dictionary<string, string>
            {
                { "AppointmentId", appointment.Id.ToString() },
                { "ServiceName", service.Name },
                { "CustomerName", user.FirstName +" " +user.FirstName }, // Add other details
                { "ProviderName", service.ProviderName },
                // ...
            };

                // 4. Send email (use a template for the body)
                string body = await File.ReadAllTextAsync("email_template.html"); // Load from file
                await SendEmailAsync(user.Email, "Your Appointment Reminder", body, mergeData);
                await SendEmailAsync(service.ProviderEmail, "Appointment Reminder", body, mergeData); // Send to provider
            }
        }
    }
}
