using HMSDataAccess.Entity;

namespace HMSDataAccess.Model
{
    public class Prescription
    {
        public int Id { get; set; }
        public string Dosage {  get; set; }
        public DateTime Date {  get; set; } 
        public int Quentity { get; set; }
        public bool IsDeleted { get; set; } = false;


        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord {  get; set; }
        public int MedicineId {  get; set; }    
        public Medicine Medicine { get; set; }  
        public string PharmasistId {  get; set; }   
        public PharmasistEntity Pharmasist { get; set; }  

    }
}
