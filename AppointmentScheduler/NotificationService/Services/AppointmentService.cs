using AppointmentService;
using CommonBase.Interfaces;

using Grpc.Core;

namespace NotificationService.Services
{
    public class AppointmentServiceClient : NotificationService.Interfaces.IAppointmentService // Your interface
    {
        private readonly AppointmentServiceRPC.AppointmentServiceRPCClient _client; // Correct client type
        private readonly ILogger<AppointmentServiceClient> _logger;

        public AppointmentServiceClient(ChannelBase channel, ILogger<AppointmentServiceClient> logger)
        {
            _client = new AppointmentServiceRPC.AppointmentServiceRPCClient(channel);
            _logger = logger;
        }

        public async Task<List<CommonBase.Models.Appointment>> GetAppointmentsForToday()
        {
            var request = new GetAppointmentsForTodayRequest();
            var appointments = new List<CommonBase.Models.Appointment>();

            try
            {
                using var call = _client.GetAppointmentsForToday(request);

                await foreach (var grpcAppointment in call.ResponseStream.ReadAllAsync())
                {
                    var appointment = new CommonBase.Models.Appointment // Your domain Appointment class
                    {
                        //Id = Guid.Parse(grpcAppointment.Id),
                        //ServiceId = Guid.Parse(grpcAppointment.ServiceId),
                        //CustomerId = Guid.Parse(grpcAppointment.CustomerId),
                        //StartTime = grpcAppointment.StartTime.ToDateTime().ToLocalTime(), // Convert to local time
                        //EndTime = grpcAppointment.EndTime.ToDateTime().ToLocalTime(),    // Convert to local time

                        Id = Guid.TryParse(grpcAppointment.Id, out var id) ? id : Guid.Empty,
                        ServiceId = Guid.TryParse(grpcAppointment.ServiceId, out var serviceId) ? serviceId : Guid.Empty,
                        CustomerId = Guid.TryParse(grpcAppointment.CustomerId, out var customerId) ? customerId : Guid.Empty,
                        StartTime = grpcAppointment.StartTime != null ? grpcAppointment.StartTime.ToDateTime().ToLocalTime() : DateTime.MinValue,
                        EndTime = grpcAppointment.EndTime != null ? grpcAppointment.EndTime.ToDateTime().ToLocalTime() : DateTime.MinValue,

                        IsConfirmed = grpcAppointment.IsConfirmed,
                        IsCancelled = grpcAppointment.IsCancelled,
                        // ... map other properties
                    };

                    appointments.Add(appointment);
                }

                return appointments;
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex, $"gRPC error: {ex.StatusCode} - {ex.Message}");
                // Handle the exception appropriately (e.g., retry, throw custom exception)
                throw; // Re-throw or return null, depending on your error handling strategy
            }
        }

        // ... other methods if needed (if your IAppointmentService has more methods)
    }
}
