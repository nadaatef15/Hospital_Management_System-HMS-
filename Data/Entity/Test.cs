namespace HMSDataAccess.Entity
{
    public class Test
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Price { get; set; }  

       public List<MedicalRecordTests> medicalRecordTests= new List<MedicalRecordTests>();
    }
}
