using HMSContracts.Model.Identity;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Users
{
    public class PatientModel :UserModel
    {
        [Required]
        public string BloodGroup { get; set; }
        public List<string>? Allergies { get; set; }
        public List<string>? MedicalHistory { get; set; }
    }
}
