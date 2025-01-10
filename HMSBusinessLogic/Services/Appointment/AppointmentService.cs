using HMSContracts.Model.Appointment;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Services.Appointment
{
    public interface IAppointmentService
    {
        void SetValues(AppointmentEntity entity, AppointmentModel model);
    }
    public class AppointmentService : IAppointmentService
    {
        public void SetValues(AppointmentEntity entity, AppointmentModel model)
        {
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.ReasonOfVisit = model.ReasonOfVisit;
            entity.Status = model.Status;
            entity.DoctorId = model.DoctorId;
            entity.PatientId = model.PatientId;
        }
    }
}
