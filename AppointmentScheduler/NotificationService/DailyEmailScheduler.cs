using Cronos;
using NotificationService.Interfaces;

namespace NotificationService
{
    public class DailyEmailScheduler : BackgroundService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<DailyEmailScheduler> _logger;
        private readonly IConfiguration _configuration;

        public DailyEmailScheduler(IEmailService emailService, ILogger<DailyEmailScheduler> logger, IConfiguration configuration)
        {
            _emailService = emailService;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Daily Email Scheduler started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                string cronExpression = _configuration.GetValue<string>("DailyEmailCron"); // Get cron from config
                if (string.IsNullOrEmpty(cronExpression))
                {
                    cronExpression = "0 8 * * *"; // Default: 8 AM daily
                }

                var cron = CronExpression.Parse(cronExpression);
                var nextRun = cron.GetNextOccurrence(DateTime.Now);

                if (nextRun.HasValue)
                {
                    var delay = nextRun.Value - DateTime.Now;

                    if (delay > TimeSpan.Zero)
                    {
                        await Task.Delay(delay, stoppingToken);
                    }

                    try
                    {
                        await _emailService.SendScheduledEmailsAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending daily emails.");
                    }

                    _logger.LogInformation("Daily emails sent.");
                }
            }

            _logger.LogInformation("Daily Email Scheduler stopped.");
        }
    }
}
