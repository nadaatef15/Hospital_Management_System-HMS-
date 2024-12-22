using HMSDataAccess.Interfaces;
using HMSDataAccess.Model;

namespace HMSDataAccess.Entity
{
    public class MedicalRecordEntity : Trackable ,ISoftDelete
    {
        public int Id {  get; set; }    

        public DateOnly Date {  get; set; }= DateOnly.FromDateTime(DateTime.Now);

        public string Treatment { get; set; }

        public int Price { get; set; }  

        public string Note { get; set; }

        public bool IsDeleted {  get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public string DoctorId { get; set; }
        public DoctorEntity Doctor { get; set; }

        public string PatientId { get; set; }
        public PatientEntity Patient { get; set; }

        public int AppointmentId {  get; set; } 
        public AppointmentEntity Appointment { get; set; }


        public  List<MedicalRecordDiagnoses> medicalRecordDiagnoses = new List<MedicalRecordDiagnoses>();

        public  List<MedicalRecordTests> medicalRecordTests = new List<MedicalRecordTests>();

        public List<PrescriptionEntity> prescriptions = new List<PrescriptionEntity>(); 

    }
}
