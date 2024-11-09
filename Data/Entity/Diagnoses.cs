namespace HMSDataAccess.Entity
{
    public class Diagnoses
    {
        public int Id {  get; set; }    

        public string Name { get; set; }    

       public List<MedicalRecordDiagnoses> medicalRecordDiagnoses = new List<MedicalRecordDiagnoses>();
    }
}
