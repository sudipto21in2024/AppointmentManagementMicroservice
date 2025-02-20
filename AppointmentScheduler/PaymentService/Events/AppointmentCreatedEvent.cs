namespace PaymentService.Events
{
    public class AppointmentCreatedEvent
    {
        public Guid AppointmentId { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        // ... other relevant properties
        public string CustomerEmail { get; set; }
        public string ProviderEmail { get; set; } // Add provider's email
    }
}
