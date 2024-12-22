using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Identity
{
    public class ChangePasswordModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string NewPassword { get; set; }

        [Compare("NewPassword" , ErrorMessage ="This Password is not matched")]
        public string ConfirmPassword { get; set;}
    }
}
