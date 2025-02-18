using AS.CQRS.Queries;
using AS.Data.Repositories;
using CommonBase.Models;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class GetAppointmentByIdQueryHandler : QueryHandler<GetAppointmentByIdQuery, Appointment?> // Note the nullable return type
    {
        private readonly IAppointmentRepository _appointmentRepository;
        // ... logger

        public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository, ILogger<GetAppointmentByIdQueryHandler> logger) : base(logger)
        {
            _appointmentRepository = appointmentRepository;
        }

        public override async Task<Appointment?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetByIdAsync(request.Id);
        }
    }
}
