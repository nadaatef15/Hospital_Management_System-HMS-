namespace HMSDataAccess.Entity
{
    public class MedicalRecord
    {
        public int Id {  get; set; }    

        public DateOnly Date {  get; set; }= DateOnly.FromDateTime(DateTime.Now);

        public string Treatment { get; set; }

        public int Price { get; set; }  

        public string Note { get; set; }

        public bool IsDeleted {  get; set; } = false;

        public string DoctorId { get; set; }
        public DoctorEntity Doctor { get; set; }

        public string PatientId { get; set; }
        public PatientEntity Patient { get; set; }

        public int AppointmentId {  get; set; } 
        public Appointment Appointment { get; set; }


      public  List<MedicalRecordDiagnoses> medicalRecordDiagnoses = new List<MedicalRecordDiagnoses>();

      public  List<MedicalRecordTests> medicalRecordTests = new List<MedicalRecordTests>();

    }
}
