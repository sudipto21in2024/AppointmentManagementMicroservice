using AS.CQRS.Commands;
using AS.CQRS.Queries;
using AS.Data.Repositories;
using AS.Interfaces;
using CommonBase.Models;
using MediatR;

namespace AS.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMediator mediator, ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            var query = new GetAllAppointmentsQuery();
            return await _mediator.Send(query);
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            var query = new GetAppointmentByIdQuery { Id = id };
            return await _mediator.Send(query);
        }

        public async Task<Guid> CreateAppointmentAsync(Appointment appointment)
        {
            var command = new CreateAppointmentCommand
            {
                StartTime = appointment.StartTime,
                ServiceId = appointment.ServiceId,
                CustomerId = appointment.CustomerId
            };

            return await _mediator.Send(command);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var command = new UpdateAppointmentCommand
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                ServiceId = appointment.ServiceId,
                CustomerId = appointment.CustomerId
            };

            await _mediator.Send(command);
        }

        public async Task CancelAppointmentAsync(Guid id)
        {
            var command = new CancelAppointmentCommand { Id = id };
            await _mediator.Send(command);
        }

        public async Task ConfirmAppointmentAsync(Guid id)
        {
            var command = new ConfirmAppointmentCommand { Id = id };
            await _mediator.Send(command);
        }

        public Task<List<Appointment>> GetAppointmentsForToday(DateTime today, DateTime tomorrow)
        {
            throw new NotImplementedException();
        }
    }
}
