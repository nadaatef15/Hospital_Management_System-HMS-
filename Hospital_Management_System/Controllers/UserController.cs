using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class UserController : BaseController
    {
        IUserManager userManager;
        public UserController(IUserManager _userManager)=>
            userManager = _userManager;
        

        [HttpPost]
        [Route("{Id}", Name = "AssignRolesToUser")]
        public async Task<IActionResult> AssignRolesToUser(string userId, List<string> roleName)
        {
            await userManager.AssignRolesToUser(userId, roleName);
            return Ok();
        }

        [HttpGet]
        [Route("{Id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await userManager.GetUserById(userId);
            return Ok(user);
        }

        [HttpDelete("id", Name = "DeleteUserById")]
        public async Task<IActionResult> DeleteUserById(string UserId)
        {
            await userManager.DeleteUser(UserId);
            return NoContent();
        }

        [HttpGet("AllUsers")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllUser()
        {
            var users = await userManager.GetAllUsers();
            return Ok(users);
        }
    }
}
