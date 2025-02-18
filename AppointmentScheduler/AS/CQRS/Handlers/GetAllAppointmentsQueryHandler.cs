using AS.CQRS.Queries;
using AS.Data.Repositories;
using CommonBase.Models;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class GetAllAppointmentsQueryHandler : QueryHandler<GetAllAppointmentsQuery, IEnumerable<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, ILogger<GetAllAppointmentsQueryHandler> logger)
            : base(logger)
        {
            _appointmentRepository = appointmentRepository;
        }

        public override async Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAllAsyncint(
                request.PageIndex,
                request.PageSize,
                request.StartDate,
                request.EndDate,
                request.UserId);
        }
    }
}
