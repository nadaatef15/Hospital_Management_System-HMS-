using HMSBusinessLogic.Manager.IdentityManager;
using HMSContracts.Model.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class RoleController : Controller
    {
        IRoleManager roleManagerIdentity;
        public RoleController(IRoleManager _roleManagerIdentity)
        {
            roleManagerIdentity = _roleManagerIdentity;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> createRole(RoleNameModel roleName)
        {
                await roleManagerIdentity.createRole(roleName);
                return Created();
        }

        [HttpDelete("Delete")]
        public async Task <IActionResult> deleteRole(string roleId)
        {
                await roleManagerIdentity.Delete(roleId);
                return Ok();
        }

        [HttpGet("GetAllRoles")]
        public async Task <IActionResult> GetAll()
        {
          var data=await roleManagerIdentity.GetAll(); 
            return Ok(data);
        }

        [HttpPut("UpdateRole")]
        public async Task <IActionResult> UpdateRoele(string roleId,RoleNameModel roleName)
        {
            await roleManagerIdentity.Update(roleId , roleName);
            return Ok();
        }

    }
}
