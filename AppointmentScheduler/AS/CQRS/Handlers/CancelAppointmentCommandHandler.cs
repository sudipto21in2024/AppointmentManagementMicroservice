using AS.CQRS.Commands;
using AS.Data.Repositories;
using AS.Events;
using CommonBase.Exception;
using MassTransit;
using MediatR;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class CancelAppointmentCommandHandler : CommandHandler<CancelAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        // ... logger

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IPublishEndpoint publishEndpoint, ILogger<CancelAppointmentCommandHandler> logger)
            : base(logger)
        {
            _appointmentRepository = appointmentRepository;
            _publishEndpoint = publishEndpoint;
        }

        public override async Task<Unit> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            // 1. Get the appointment (optional - you might just delete by ID)
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                throw new EntityNotFoundException($"Appointment with ID {request.Id} not found."); // Custom Exception
            }

            // 2. Perform cancellation logic (e.g., update IsCancelled = true)
            appointment.IsCancelled = true; // Or perhaps call a dedicated Cancel method in the domain
            await _appointmentRepository.UpdateAsync(appointment); // Update the appointment

            // 3. Publish the event
            var cancelledEvent = new AppointmentCancelledEvent { AppointmentId = request.Id };
            await _publishEndpoint.Publish(cancelledEvent, cancellationToken);

            return Unit.Value;
        }
    }
}
