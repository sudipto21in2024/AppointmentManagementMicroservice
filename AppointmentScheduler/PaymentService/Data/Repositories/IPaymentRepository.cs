using CommonBase.Models;

namespace PaymentService.Data.Repositories
{
  
    public interface IPaymentRepository
    {
        Task<Payment> GetByAppointmentIdAsync(Guid appointmentId);
        Task CreateAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        // ... other repository methods as needed
    }
}
