using System.ComponentModel.DataAnnotations;

namespace HMSDataAccess.Entity
{
    public class Specialties
    {
        public int Id { get; set; }

        public string Name { get; set; }

       public List<DoctorSpecialties> doctorSpecialties = new List<DoctorSpecialties>();
    }
}
