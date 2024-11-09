using HMSBusinessLogic.Manager.LabTechnician;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class LabTechnicianController : BaseController
    {
        private readonly ILabTechnicianManager _labTechnicianManager;
        public LabTechnicianController(ILabTechnicianManager labTechnicianManager)
        {
            _labTechnicianManager = labTechnicianManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] labTechnicianModel user)
        {
            await _labTechnicianManager.Register(user);
            return Created();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id, [FromForm] UserModel user)
        {
            await _labTechnicianManager.Update(id, user);
            return Ok();
        }
    }
}
