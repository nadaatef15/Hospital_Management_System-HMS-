using Data.Entity;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSDataAccess.Repo
{
    public interface IAppointmentRepo
    {
        Task CreatAppointment(Appointment appointment);
        Task Delete(int AppointmentId);
        Task<Appointment> GetById(int id);
        List<Appointment> GetAll();
    }
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly HMSDBContext _dbcontext;
        public AppointmentRepo(HMSDBContext context)
        {
            _dbcontext = context;
        }

        public async Task CreatAppointment(Appointment appointment)
        {
            await _dbcontext.Appointments.AddAsync(appointment);
            await Reposatory.SaveAsync(_dbcontext);
        }

        public async Task Delete(int appointmentId)
        {
            var target = await _dbcontext.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId) ??
                throw new NotFoundException(appointmentDoesnotExist);

            var result = _dbcontext.MedicalRecord.Where(a => a.AppointmentId == target.Id);

            if (result is not null)
                throw new ConflictException(invalidAppointment);

            target.IsDeleted = true;
            await Reposatory.SaveAsync(_dbcontext);

        }

        public async Task<Appointment> GetById(int id)
        {
            var appointment = await _dbcontext.Appointments.FirstOrDefaultAsync(a => a.Id == id) ??
                  throw new NotFoundException(appointmentDoesnotExist);

            return appointment;
        }

        public List<Appointment> GetAll()=>
              _dbcontext.Appointments.ToList();
        

    }
}
