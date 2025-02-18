using CommonBase.Infrastructure.CQRS.BaseClasses;

namespace AS.CQRS.Commands
{
    public class CreateAppointmentCommand : Command<Guid>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
