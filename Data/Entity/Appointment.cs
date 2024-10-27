using static HMSContracts.Constants.SysEnums;

namespace HMSDataAccess.Entity
{
    public class Appointment
    {
        public int Id { get; set; } 

        public DateOnly Date {  get; set; }

        public TimeOnly SartTime { get; set; }

        public TimeOnly EndTime { get; set; }   

        public string ReasonOfVisit { get; set; }

        public Status Status { get; set; }

        public string DoctorId {  get; set; }   
        public Doctor Doctor { get; set; }  

        public string PatientId {  get; set; }  
        public Patient Patient { get; set; }    
    }
}
