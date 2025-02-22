using AS.CQRS.Queries;
using AS.Data.Repositories;
using CommonBase.DTO;
using CommonBase.Models;
using SharedLibrary.CQRS.Handlers;

namespace AS.CQRS.Handlers
{
    public class GetAllAppointmentsQueryHandler : QueryHandler<GetAllAppointmentsQuery, AppointmentResponseDTO>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, ILogger<GetAllAppointmentsQueryHandler> logger)
            : base(logger)
        {
            _appointmentRepository = appointmentRepository;
        }

        public override async Task<AppointmentResponseDTO> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var (appointments, totalRecords) = await _appointmentRepository.GetAllAppointmentsWithTotalCountAsync(
                request.PageIndex,
                request.PageSize,
                request.StartDate,
                request.EndDate,
                request.UserId,
                request.ProviderId
            );

            var appointmentDtos = appointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                ServiceId = a.ServiceId,
                CustomerId = a.CustomerId,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                IsConfirmed = a.IsConfirmed,
                IsCancelled = a.IsCancelled,
                ServiceName = a.Service.Name,
                CustomerName = a.Customer.FirstName +" " + a.Customer.LastName
                // ... map other properties
            }).ToList();

            return new AppointmentResponseDTO
            {
                Data = appointmentDtos,
                TotalRecords = totalRecords
            };
        }
    }
}
