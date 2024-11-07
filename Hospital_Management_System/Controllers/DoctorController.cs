using HMSBusinessLogic.Manager.Doctor;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IDoctorManager _doctorManager;
        public DoctorController(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] DoctorModel user)
        {
            await  _doctorManager.Register(user);
            return Created();
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id, [FromForm] ModifiedDoctor user)
        {
            await _doctorManager.Update(id,user);
            return Ok();
        }
    }
}
