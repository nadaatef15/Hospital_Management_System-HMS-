using HMSBusinessLogic.Manager.Patient;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : BaseController
    {
        private readonly IPatientsManager _patientsManager;
        public PatientController(IPatientsManager patientsManager)
        {
            _patientsManager = patientsManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] PatientModel user)
        {
            await _patientsManager.Register(user);
            return Created();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id, [FromForm] UserModel user)
        {
            await _patientsManager.Update(id, user);
            return Ok();
        }
    }
}
