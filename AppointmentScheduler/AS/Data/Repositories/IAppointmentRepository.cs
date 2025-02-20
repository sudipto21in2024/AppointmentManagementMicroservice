using CommonBase.Models;

namespace AS.Data.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsyncint (int pageIndex = 0,
            int pageSize = 10,
            DateTime? startDate = null,
            DateTime? endDate = null,
            Guid? userId = null);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Guid id);
        Task<bool> IsAppointmentSlotBooked(DateTime startTime, DateTime endTime, Guid? excludeAppointmentId = null);
        Task<List<Appointment>> GetAppointmentsForToday(DateTime today, DateTime tomorrow);
    }
}
