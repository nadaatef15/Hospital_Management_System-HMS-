using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Services.PatientService
{
    public interface IPatientService
    {
        void SetValues(PatientEntity entity, PatientModel model);
    }
    public class PatientService : IPatientService
    {
        public void SetValues(PatientEntity entity, PatientModel model)
        {
            entity.PhoneNumber = model.Phone;
            entity.Address = model.Address;
            entity.Age = model.Age;
            entity.UserName = model.UserName;
            entity.Email = model.Email;
            entity.Gender = model.Gender == 'M' ? Gender.M : Gender.F;
            entity.BloodGroup = model.BloodGroup;
            entity.Allergies = model.Allergies?.ToList();
            entity.MedicalHistory = model.MedicalHistory?.ToList();
        }
    }
}
