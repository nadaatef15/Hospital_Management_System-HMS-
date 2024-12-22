namespace HMSBusinessLogic.Resource
{
    public class PatientResource :UserResource
    {

        public string BloodGroup { get; set; }
        public List<string>? Allergies { get; set; }
        public List<string>? MedicalHistory { get; set; }

    }
}
