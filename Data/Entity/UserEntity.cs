using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using static HMSContracts.Constants.SysEnums;

namespace HMSDataAccess.Entity
{
    public class UserEntity : IdentityUser
    {
        public string Address { get; set; }

        public string? ImagePath { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
