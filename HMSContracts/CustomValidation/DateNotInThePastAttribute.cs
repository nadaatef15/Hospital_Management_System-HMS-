using HMSContracts.Model.Appointment;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.CustomValidation
{
    public class DateNotInThePastAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // return base.IsValid(value, validationContext);
            var model = (AppointmentModel)validationContext.ObjectInstance;

            if (value is not DateOnly)
            {
                return new ValidationResult("Invalid date format.");
            }

            if (model.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("The date must be today or a future date.");

            }
            return ValidationResult.Success;

        }
    }
}
