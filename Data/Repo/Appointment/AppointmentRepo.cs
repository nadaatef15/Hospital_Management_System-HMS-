using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace HMSDataAccess.Repo.Appointment
{
    public interface IAppointmentRepo
    {
        Task CreateAppointment(AppointmentEntity appointment);
        Task DeleteAppointment(AppointmentEntity Appointment);
        Task<AppointmentEntity?> GetAppointmentByIdAsNoTracking(int id);
        Task<AppointmentEntity?> GetAppointmentById(int id);
        Task<List<AppointmentEntity>> GetAllAppointments();
        Task saveChanges();
    }
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly HMSDBContext _dbContext;
        public AppointmentRepo(HMSDBContext context) =>
            _dbContext = context;


        public async Task CreateAppointment(AppointmentEntity appointment)
        {
            await _dbContext.Appointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAppointment(AppointmentEntity appointment)
        {
            _dbContext.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<AppointmentEntity?> GetAppointmentByIdAsNoTracking(int id) =>
            await _dbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<AppointmentEntity?> GetAppointmentById(int id) =>
           await _dbContext.Appointments.FindAsync(id);

        public async Task<List<AppointmentEntity>> GetAllAppointments() =>
             await _dbContext.Appointments.AsNoTracking().ToListAsync();

        public async Task saveChanges() =>
            await _dbContext.SaveChangesAsync();

    }
}
