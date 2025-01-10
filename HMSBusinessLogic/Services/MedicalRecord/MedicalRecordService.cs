using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Services.MedicalRecord
{
    public interface IMedicalRecordService
    {
        void SetValues(MedicalRecordEntity entity, MedicalRecordModel model);
    }
    public class MedicalRecordService : IMedicalRecordService
    {
        public void SetValues(MedicalRecordEntity entity, MedicalRecordModel model)
        {
            entity.Treatment = model.Treatment;
            entity.Price = model.Price;
            entity.Note = model.Note;
            entity.PatientId = model.PatientId;
            entity.DoctorId = model.DoctorId;
            entity.AppointmentId = model.AppointmentId;
        }
    }
}
