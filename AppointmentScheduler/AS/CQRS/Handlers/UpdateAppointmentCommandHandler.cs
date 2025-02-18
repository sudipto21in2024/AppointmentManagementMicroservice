using AS.CQRS.Commands;
using AS.Data.Repositories;
using MediatR;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class UpdateAppointmentCommandHandler : CommandHandler<UpdateAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, ILogger<UpdateAppointmentCommandHandler> logger)
            : base(logger)
        {
            _appointmentRepository = appointmentRepository;
        }

        public override async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(request.Id);

            if (existingAppointment == null)
            {
                _logger.LogWarning($"Appointment with ID: {request.Id} not found.");
                return Unit.Value;
            }

            // Check for overlapping appointments (excluding the current appointment)
            if (await _appointmentRepository.IsAppointmentSlotBooked(request.StartTime, request.EndTime, existingAppointment.Id))
            {
                _logger.LogWarning($"Appointment slot is already booked.");
                throw new InvalidOperationException("Appointment slot is already booked.");
            }

            existingAppointment.StartTime = request.StartTime;
            existingAppointment.EndTime = request.EndTime;
            existingAppointment.ServiceId = request.ServiceId;
            existingAppointment.CustomerId = request.CustomerId;

            await _appointmentRepository.UpdateAsync(existingAppointment);
            return Unit.Value;
        }
    }
}
