using HMSContracts.CustomValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using static HMSContracts.Constants.SysEnums;

namespace HMSContracts.Model.Appointment
{
    public class AppointmentModel
    {

        [Required(ErrorMessage ="the format is HH:MM:SS")]
        [SwaggerSchema(Format = "time", Description = "Time in HH:mm:ss format" )]
        public TimeOnly SartTime { get; set; }

        [Required(ErrorMessage = "the format is HH:MM:SS")]
        [SwaggerSchema(Format = "time", Description = "Time in HH:mm:ss format")]
        [EndTimeAfterStartTime]
        public TimeOnly EndTime { get; set; }

        [Required]
        public string ReasonOfVisit { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public string DoctorId { get; set; }

        [Required]
        public string PatientId { get; set; }

    }
}
