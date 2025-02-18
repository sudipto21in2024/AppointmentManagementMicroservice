using AS.CQRS.Commands;
using AS.Data.Repositories;
using AS.Events;
using CommonBase.Exception;
using MassTransit;
using MediatR;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class ConfirmAppointmentCommandHandler : CommandHandler<ConfirmAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ConfirmAppointmentCommandHandler> _logger;

        public ConfirmAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IPublishEndpoint publishEndpoint,
            ILogger<ConfirmAppointmentCommandHandler> logger)
            : base(logger)
        {
            _appointmentRepository = appointmentRepository;
            _publishEndpoint = publishEndpoint;
        }

        public override async Task<Unit> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                throw new EntityNotFoundException($"Appointment with ID {request.Id} not found.");
            }

            if (appointment.IsConfirmed) // Check if already confirmed
            {
                _logger.LogWarning($"Appointment with ID {request.Id} is already confirmed.");
                return Unit.Value; // Or throw an exception if you want to prevent double confirmation
            }

            if (appointment.IsCancelled) // You might want to prevent confirming a cancelled appointment
            {
                throw new InvalidOperationException($"Cannot confirm a cancelled appointment (ID: {request.Id}).");
            }

            // Update the appointment status (and any other relevant fields)
            appointment.IsConfirmed = true;
            await _appointmentRepository.UpdateAsync(appointment);

            // Publish the AppointmentConfirmedEvent
            var confirmedEvent = new AppointmentConfirmedEvent { AppointmentId = request.Id };
            await _publishEndpoint.Publish(confirmedEvent, cancellationToken);

            _logger.LogInformation($"Appointment with ID {request.Id} confirmed.");

            return Unit.Value;
        }
    }
}
