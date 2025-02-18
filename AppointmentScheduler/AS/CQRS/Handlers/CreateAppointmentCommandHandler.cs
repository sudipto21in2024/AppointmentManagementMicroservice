using AS.CQRS.Commands;
using AS.Data.Repositories;
using AS.Events;
using CommonBase.Models;
using MassTransit;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class CreateAppointmentCommandHandler : CommandHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPublishEndpoint _publishEndpoint; // Add this field

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IPublishEndpoint publishEndpoint, ILogger<CreateAppointmentCommandHandler> logger)
            : base(logger)
        {
            _appointmentRepository = appointmentRepository;
        }

        public override async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Check for overlapping appointments
            if (await _appointmentRepository.IsAppointmentSlotBooked(request.StartTime, request.EndTime))
            {
                _logger.LogWarning($"Appointment slot is already booked.");
                throw new InvalidOperationException("Appointment slot is already booked.");
            }

            var appointment = new Appointment
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                ServiceId = request.ServiceId,
                CustomerId = request.CustomerId
            };

            var appointmentId = await _appointmentRepository.CreateAsync(appointment);
            // Publish the event
            var appointmentCreatedEvent = new AppointmentCreatedEvent
            {
                AppointmentId = appointmentId,
                ServiceId = request.ServiceId,
                // ... any other relevant data
            };

            await _publishEndpoint.Publish(appointmentCreatedEvent, cancellationToken); // Publish the event
            return appointmentId;
        }
    }
}
