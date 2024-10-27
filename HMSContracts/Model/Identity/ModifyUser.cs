using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Identity
{
    public class ModifyUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [Range(maximum: 100, minimum: 1)]
        public int Age { get; set; }
        [Required]
        [RegularExpression("^[FM]$", ErrorMessage = "Gender must be 'F' or 'M'.")]
        public char Gender { get; set; }
        [Required]
        // [RegularExpression("^01[0-5]\\d{8}$\r\n")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }

        public IFormFile? Image { get; set; }

    }
}
