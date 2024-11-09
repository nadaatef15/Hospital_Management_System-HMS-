using HMSBusinessLogic.Manager.Receptionist;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class ReceptionistController : BaseController
    {
        private readonly IReceptionistManager _receptionistManager;
        public ReceptionistController(IReceptionistManager receptionistManager)
        {
            _receptionistManager = receptionistManager;
        }

        [HttpPost("Register")]
        public async Task< IActionResult> Register([FromForm] ReceptionistModel user)
        {
            await _receptionistManager.Register(user);
            return Created();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id ,  [FromForm] UserModel user)
        {
            await _receptionistManager.Update(id,user);
            return Ok();
        }
    }
}
