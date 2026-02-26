using AppointmentPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentPlanner.Api.Data.Repository
{
    public class AppointmentPlannerRepository
    {
        private readonly AppointmentPlannerDbContext _context;

        public AppointmentPlannerRepository(AppointmentPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentRequest> AddAppointmentAsync(AppointmentRequest appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> AddAppointmentAsync(string name, string email, string cellNumber, 
            DateTime appointmentDate, DateTime appointmentTime, bool hasBusiness)
        {
            var appointment = new AppointmentRequest
            {
                Name = name,
                Email = email,
                CellNumber = cellNumber,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime,
                HasBusiness = hasBusiness
            };

            _context.Appointments.Add(appointment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
