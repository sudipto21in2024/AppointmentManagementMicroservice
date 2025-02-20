namespace PaymentService.Interfaces
{
    public interface IPaymentService
    {
        Task CreatePendingPaymentAsync(
            Guid appointmentId, Guid serviceId, string serviceName, decimal servicePrice, DateTime appointmentStartTime, DateTime appointmentEndTime);

        Task UpdatePaymentStatusAsync(Guid appointmentId, string paymentStatus);
        // ... other methods
    }
}
