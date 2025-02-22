using CommonBase.DTO;
using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace AS.CQRS.Queries
{
    public class GetAllAppointmentsQuery : Query<AppointmentResponseDTO>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProviderId { get; set; }

    }
}
