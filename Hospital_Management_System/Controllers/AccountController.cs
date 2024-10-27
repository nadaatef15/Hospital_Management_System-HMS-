using HMSBusinessLogic.Manager.AccountManager;
using HMSContracts.Model.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Hospital_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager accounntManager;
        public AccountController(IAccountManager _accounntManager) =>
            accounntManager = _accounntManager;


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var token = await accounntManager.Login(loginModel);
            if (token is not null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]UserModel userModel)
        {
                await accounntManager.Register(userModel);
                return Created();
        }
    }
}
