using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HMSDataAccess.Entity
{
    [PrimaryKey(nameof(TestId), nameof(MedicalRecordId))]
    public class MedicalRecordTests
    {
        [Key]
        public int TestId {  get; set; }
        [Key]
        public int MedicalRecordId {  get; set; } 

        public string LabTechnicianId {  get; set; }
        public LabTechnicianEntity LabTechnician { get; set; }  

        public string TestResult {  get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
