namespace HMSDataAccess.Entity
{
    public class MedicalRecord
    {
        public int Id {  get; set; }    

        public DateOnly Date {  get; set; } 

        public string Treatment { get; set; }

        public int Price { get; set; }  

        public string Note { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AppointmentId {  get; set; } 
        public Appointment Appointment { get; set; }


      public  List<MedicalRecordDiagnoses> medicalRecordDiagnoses = new List<MedicalRecordDiagnoses>();

      public  List<MedicalRecordTests> medicalRecordTests = new List<MedicalRecordTests>();

    }
}
