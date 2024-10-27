using static HMSContracts.Constants.SysEnums;
namespace HMSDataAccess.Entity
{
    public class Invoice
    {
        public int Id { get; set; } 
        public DateOnly Date {  get; set; } 
        public int Total { get; set; }  
        public Status Status { get; set; }

        public string PatientId {  get; set; }  
        public Patient Patient { get; set; }

        public int AppointmentId {  get; set; } 
        public Appointment Appointment { get; set; }

    }
}
