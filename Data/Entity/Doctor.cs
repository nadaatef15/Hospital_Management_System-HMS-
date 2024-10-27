using HMSDataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HMSDataAccess.Entity
{
    public class Doctor
    {
        public Doctor()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }

      public List<DoctorSpecialties> doctorSpecialties = new List<DoctorSpecialties>();
    }
}
