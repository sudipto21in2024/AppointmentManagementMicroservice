using CommonBase.Exception;
using CommonBase.Models;
using PaymentService.Data.Repositories;
using PaymentService.Interfaces;

namespace PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailService _emailService;

        public PaymentService(IPaymentRepository paymentRepository, IEmailService emailService)
        {
            _paymentRepository = paymentRepository;
            _emailService = emailService;
        }

        public async Task CreatePendingPaymentAsync(
            Guid appointmentId, Guid serviceId, string serviceName, decimal servicePrice, DateTime appointmentStartTime, DateTime appointmentEndTime)
        {
            var payment = new Payment
            {
                AppointmentId = appointmentId,
                ServiceId = serviceId,
                ServiceName = serviceName,
                Amount = servicePrice,
                Status = "Pending",
                AppointmentStartTime = appointmentStartTime,
                AppointmentEndTime = appointmentEndTime
            };

            await _paymentRepository.CreateAsync(payment);
        }

        public async Task UpdatePaymentStatusAsync(Guid appointmentId, string paymentStatus)
        {
            var payment = await _paymentRepository.GetByAppointmentIdAsync(appointmentId);
            if (payment == null)
            {
                throw new EntityNotFoundException($"Payment for appointment {appointmentId} not found.");
            }

            payment.Status = paymentStatus;
            await _paymentRepository.UpdateAsync(payment);

            if (paymentStatus == "Paid")
            {
                await _emailService.SendInvoiceEmailAsync(payment);
            }
        }

        // ... other methods
    }
}
