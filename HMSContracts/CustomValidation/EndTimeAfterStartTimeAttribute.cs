using HMSContracts.Model.Appointment;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.CustomValidation
{
    public class EndTimeAfterStartTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // return base.IsValid(value, validationContext);
            var model = (AppointmentModel)validationContext.ObjectInstance;

            if (model.EndTime < model.StartTime)
            {
                return new ValidationResult("EndTime must be after StartTime.");
            }

            return ValidationResult.Success;
        }
    }
}
