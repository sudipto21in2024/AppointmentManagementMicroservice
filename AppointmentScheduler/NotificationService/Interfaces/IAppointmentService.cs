using AppointmentService;

namespace NotificationService.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<CommonBase.Models.Appointment>> GetAppointmentsForToday();
        //Task<Appointment> GetAppointmentByIdAsync(Guid id);
    }
}
