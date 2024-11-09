using HMSBusinessLogic.Manager.IdentityManager;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class UserController : BaseController
    {
        IUserManager userManager;
        public UserController(IUserManager _userManager)
        {
            userManager = _userManager;
        }

        [HttpPost("AssignRolesToUser")]
        public async Task<IActionResult> AssignRolesToUser(string userId, List<string> roleName)
        {
            await userManager.AssignRolesToUser(userId, roleName);
            return Ok();
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await userManager.GetUserById(userId);
            return Ok(user);
        }

        [HttpDelete("DeleteRoleById")]
        public async Task<IActionResult> Delete(string UserId)
        {
            await userManager.DeleteUser(UserId);
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Updateuser(UserEntity user, [FromForm] UserModel userModel)
        {
            await userManager.UpdateUser(user, userModel);
            return Ok();
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await userManager.GetAllUsers();
            return Ok(users);
        }
    }
}
