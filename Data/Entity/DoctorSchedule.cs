namespace HMSDataAccess.Entity
{
    public class DoctorSchedule
    {
        public int Id { get; set; }

        public DateOnly Date {  get; set; }

        public TimeOnly StartTime { get; set; } 

        public TimeOnly EndTime { get; set; }

        public string DoctorId {  get; set; }
        public Doctor Doctor { get; set; }  
    }
}
