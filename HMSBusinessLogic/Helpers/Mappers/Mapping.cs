using HMSBusinessLogic.Resource;
using HMSContracts.Model.Appointment;
using HMSContracts.Model.Identity;
using HMSContracts.Model.MedicalRecord;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class Mapping
    {
        public static UserEntity ToEntity(this UserModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };


        public static ReceptionistEntity ToReceptionistEntity(this ReceptionistModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };

        public static LabTechnicianEntity ToLabTechEntity(this labTechnicianModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };


        public static DoctorEntity ToDoctorEntity(this DoctorModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F,
            Salary = user.Salary,
            
        };


        public static PatientEntity ToPatientEntity(this PatientModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };

        public static MedicalRecord ToMedicalRecord(this MedicalRecordModel model) => new()
        {
        
            Treatment= model.Treatment,
            Price=model.Price,
            Note=model.Note,
            PatientId=model.PatientId,
            DoctorId=model.DoctorId,
            AppointmentId=model.AppointmentId,
        };

        public static Appointment ToAppointment(this AppointmentModel model) => new()
        {
            //Date = model.Date,
            SartTime =model.SartTime,
            EndTime=model.EndTime,
            ReasonOfVisit=model.ReasonOfVisit,
            Status=model.Status,
            DoctorId= model.DoctorId,
            PatientId= model.PatientId,
        };

        public static UserResource ToResource(this UserEntity user) => new()
        {
            UserName = user.UserName,
            Phone = user.PhoneNumber,
            Email = user.Email,
            Age = user.Age,
            Address = user.Address,
            Image = user.ImagePath,
            Gender = user.Gender.ToString(),

        };


        public static MedicalRecordModel ToMRModel(this MedicalRecord model) => new()
        {
            Treatment = model.Treatment,
            Price = model.Price,
            Note = model.Note,
            PatientId = model.PatientId,
            DoctorId = model.DoctorId,
            AppointmentId = model.AppointmentId,
        };

        public static AppointmentModel ToAppointmentModel(this Appointment model) => new()
        {
            //Date = model.Date,
            SartTime = model.SartTime,
            EndTime = model.EndTime,
            ReasonOfVisit = model.ReasonOfVisit,
            DoctorId = model.DoctorId,
            PatientId = model.PatientId,
            Status = model.Status == 0 ? Status.complete : Status.incomplete,
        };

    }

}
