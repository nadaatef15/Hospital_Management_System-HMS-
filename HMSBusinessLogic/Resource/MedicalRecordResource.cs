namespace HMSBusinessLogic.Resource
{
    public class MedicalRecordResource
    {
        public string Treatment { get; set; }

        public int Price { get; set; }

        public string Note { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public int AppointmentId { get; set; }
    }
}
