using Data.Entity;
using HMSBusinessLogic.Helpers.Mappers;
using HMSContracts.Model.Appointment;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Appointment
{
    public interface IAppointmentManager
    {
       Task CreateAppointment(AppointmentModel model);
       Task Delete(int id);
       Task<AppointmentModel> GetById(int id);
       List<AppointmentModel> GetAll();
    }
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly UserManager<UserEntity> _userManager;
        public AppointmentManager(IAppointmentRepo appointmentRepo , UserManager<UserEntity> userManager)
        {
            _appointmentRepo= appointmentRepo;
            _userManager= userManager;
        }

        public async Task CreateAppointment(AppointmentModel model)
        {
            var doc = await _userManager.FindByIdAsync(model.DoctorId);
            var patient = await _userManager.FindByIdAsync(model.PatientId);

            if (doc is null || patient is null)
                throw new NotFoundException(UseDoesnotExist);

            var appointment = model.ToAppointment();

            await _appointmentRepo.CreatAppointment(appointment);
        }

        public async Task Delete(int id)
        {
            await _appointmentRepo.Delete(id);
        }

        public async Task<AppointmentModel> GetById(int id)
        {
            var appointment = await _appointmentRepo.GetById(id);
            return appointment.ToAppointmentModel(); 
        }

        public List<AppointmentModel> GetAll()
        {
            var result = _appointmentRepo.GetAll();
            List<AppointmentModel> appointmentModels = new();
            result.ForEach(a => appointmentModels.Add(a.ToAppointmentModel()));
            return appointmentModels;
        }
    }
}
