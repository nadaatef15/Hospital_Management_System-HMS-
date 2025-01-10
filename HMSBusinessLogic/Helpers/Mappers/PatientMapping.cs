using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class PatientMapping
    {
        public static PatientEntity ToEntity(this PatientModel model) => new()
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.Phone,
            Age = model.Age,
            Address = model.Address,
            Gender = model.Gender == 'M' ? Gender.M : Gender.F,
            BloodGroup= model.BloodGroup,
            Allergies= model.Allergies?.ToList(),
            MedicalHistory= model.MedicalHistory?.ToList()

        };

        public static PatientResource ToPatientResource(this PatientEntity entity) => new()
        {
            Id = entity.Id,
            UserName = entity.UserName!,
            Phone = entity.PhoneNumber!,
            Email = entity.Email!,
            Age = entity.Age,
            Address = entity.Address,
            Image = entity.ImagePath?.ToString(),
            Gender = entity.Gender.ToString(),
            BloodGroup = entity.BloodGroup,
            Allergies = entity.Allergies?.ToList(),
            MedicalHistory = entity.MedicalHistory?.ToList()
        };

    }
}
