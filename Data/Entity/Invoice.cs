using static HMSContracts.Constants.SysEnums;
namespace HMSDataAccess.Entity
{
    public class Invoice
    {
        public int Id { get; set; } 
        public DateOnly Date {  get; set; } 
        public int Total { get; set; }  
        public Status Status { get; set; }
        public bool IsDeleted { get; set; } = false;

        public string PatientId {  get; set; }  
        public PatientEntity Patient { get; set; }

        public int AppointmentId {  get; set; } 
        public Appointment Appointment { get; set; }

    }
}
