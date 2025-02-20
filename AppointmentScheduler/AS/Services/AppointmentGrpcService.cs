using AS.Data.Repositories;
using CommonBase.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;

namespace AppointmentService.Services
{
    public class AppointmentGRPCService : AppointmentServiceRPC.AppointmentServiceRPCBase // Inherit from generated gRPC class
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILogger<AppointmentGRPCService> _logger; // Use a separate logger

        public AppointmentGRPCService(IAppointmentRepository appointmentRepository, ILogger<AppointmentGRPCService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public override async Task GetAppointmentsForToday(GetAppointmentsForTodayRequest request, IServerStreamWriter<Appointment> responseStream, ServerCallContext context)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            var appointments = await _appointmentRepository.GetAppointmentsForToday(today, tomorrow);

            foreach (var appointment in appointments)
            {
                var grpcAppointment = new Appointment // gRPC Appointment message
                {
                    Id = appointment.Id.ToString(),
                    ServiceId = appointment.ServiceId.ToString(),
                    CustomerId = appointment.CustomerId.ToString(),
                    StartTime = Timestamp.FromDateTime(appointment.StartTime.ToUniversalTime()),
                    EndTime = Timestamp.FromDateTime(appointment.EndTime.ToUniversalTime()),
                    IsConfirmed = appointment.IsConfirmed,
                    IsCancelled = appointment.IsCancelled
                };

                await responseStream.WriteAsync(grpcAppointment);
            }
        }
    }
}


 
