using HMSBusinessLogic.Resource;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class MedicalRecordMapping
    {
        public static MedicalRecordResource ToResource(this MedicalRecordEntity model) => new()
        {
            Treatment = model.Treatment,
            Price = model.Price,
            Note = model.Note,
            PatientId = model.PatientId,
            DoctorId = model.DoctorId,
            AppointmentId = model.AppointmentId,
        };

        public static MedicalRecordEntity ToEntity(this MedicalRecordModel model) => new()
        {

            Treatment = model.Treatment,
            Price = model.Price,
            Note = model.Note,
            PatientId = model.PatientId,
            DoctorId = model.DoctorId,
            AppointmentId = model.AppointmentId,
        };

    }
}
