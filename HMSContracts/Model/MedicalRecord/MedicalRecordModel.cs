using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMSContracts.Model.MedicalRecord
{
    public class MedicalRecordModel
    {


        [Required]
        public string Treatment { get; set; }

        [Required]
        public int Price { get; set; }

        public string Note { get; set; }

        [Required]
        public string DoctorId { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public int AppointmentId { get; set; }
      
    }
}
