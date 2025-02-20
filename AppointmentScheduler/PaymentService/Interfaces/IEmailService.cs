using CommonBase.Models;

namespace PaymentService.Interfaces
{
    public interface IEmailService
    {
        Task SendInvoiceEmailAsync(Payment payment);
    }
}
