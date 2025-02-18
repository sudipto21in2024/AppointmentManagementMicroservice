namespace AS.Events
{
    public class AppointmentCreatedEvent
    {
        public Guid AppointmentId { get; set; }
        public Guid ServiceId { get; set; }
        public decimal ServicePrice { get; set; }
    }
}
