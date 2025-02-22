using CommonBase.DTO;
using CommonBase.Models;

namespace AS.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDTO> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(Guid id);
        Task<Guid> CreateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task CancelAppointmentAsync(Guid id);
        Task ConfirmAppointmentAsync(Guid id);
        Task<List<Appointment>> GetAppointmentsForToday(DateTime today, DateTime tomorrow);
    }
}
