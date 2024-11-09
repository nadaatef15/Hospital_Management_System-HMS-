namespace HMSDataAccess.Entity
{
    public class DoctorEntity : UserEntity
    {
        public List<DoctorSpecialties> doctorSpecialties = [];
        public int? Salary { get; set; }
    }
}
