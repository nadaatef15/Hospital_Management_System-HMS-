using static HMSContracts.Constants.SysEnums;

namespace HMSDataAccess.Entity
{
    public class Appointment
    {
        public int Id { get; set; } 

        public DateOnly Date {  get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public TimeOnly SartTime { get; set; }

        public TimeOnly EndTime { get; set; }   

        public string ReasonOfVisit { get; set; }

        public Status Status { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string DoctorId {  get; set; }   

        public DoctorEntity Doctor { get; set; }  

        public string PatientId {  get; set; }  

        public PatientEntity Patient { get; set; }

      

    }
}
