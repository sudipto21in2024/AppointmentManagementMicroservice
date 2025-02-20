using CommonBase.Data;
using CommonBase.Models;
using Microsoft.EntityFrameworkCore;

namespace AS.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Appointment>> GetAllAsync()
        //{
        //    return await _context.Appointments.ToListAsync();
        //}
     
        public async Task<IEnumerable<Appointment>> GetAllAsyncint(
            int pageIndex = 0,
            int pageSize = 10,
            DateTime? startDate = null,
            DateTime? endDate = null,
            Guid? userId = null)
        {
            IQueryable<Appointment> query = _context.Appointments
                .Include(a => a.Service) // Include related entities if needed
                .Include(a => a.Customer);

            if (startDate.HasValue)
            {
                query = query.Where(a => a.StartTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(a => a.EndTime <= endDate.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(a => a.CustomerId == userId.Value);
            }

            return await query
                .OrderBy(a => a.StartTime) // Order by start time (or any other relevant field)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<Guid> CreateAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment.Id;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAppointmentSlotBooked(DateTime startTime, DateTime endTime, Guid? excludeAppointmentId = null)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    (a.StartTime >= startTime && a.StartTime < endTime) ||
                    (a.EndTime > startTime && a.EndTime <= endTime) ||
                    (a.StartTime <= startTime && a.EndTime >= endTime) &&
                    (excludeAppointmentId == null || a.Id != excludeAppointmentId));
        }
        public async Task<List<Appointment>> GetAppointmentsForToday(DateTime today, DateTime tomorrow)
        {
            return await _context.Appointments
                .Where(a => a.StartTime >= today && a.StartTime < tomorrow)
                .ToListAsync();
        }


    }
}
