using CommonBase.Data;
using CommonBase.Models;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Data.Repositories
{
   
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);
        }

        public async Task CreateAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment); // Or _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // ... other methods
    }
}
