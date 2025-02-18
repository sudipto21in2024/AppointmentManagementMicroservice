using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace AS.CQRS.Queries
{
    public class GetAppointmentByIdQuery : Query<Appointment?> // Matches the handler's return type
    {
        public Guid Id { get; set; }
    }
}
