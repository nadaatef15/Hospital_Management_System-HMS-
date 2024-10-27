namespace HMSContracts.Constants
{
    public static class SysEnums
    {
        public enum Status
        {
            complete,
            incomplete
        }

        public enum Gender
        {
            F,
            M
        }

        public enum model
        {
            User,Medicine,Payment,Specialties,Test,Invoice,Appointment,Prescription,Diagnoses,MedicalRecord
        }

        public enum permissionType
        {
            View,
            Create,
            Edit,
            Delete,
        }
    }
}
