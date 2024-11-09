using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Identity
{
    public class ModifyUser
    {
        public string UserName { get; set; }
       
        [Range( minimum: 1, maximum: 100)]
        public int Age { get; set; }

      
        [RegularExpression("^[FM]$", ErrorMessage = "Gender must be 'F' or 'M'.")]
        public char Gender { get; set; }

     
        // [RegularExpression("^01[0-5]\\d{8}$\r\n")]
        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email {  get; set; }

        public IFormFile? Image { get; set; }

    }
}
