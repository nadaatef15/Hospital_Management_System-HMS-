using HMSBusinessLogic.Manager.LabTechnician;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class LabTechnicianController : BaseController
    {
        private readonly ILabTechnicianManager _labTechnicianManager;
        public LabTechnicianController(ILabTechnicianManager labTechnicianManager)=>
            _labTechnicianManager = labTechnicianManager;
        

        [HttpPost]
        [Route("RegisterLabTech")]
        public async Task<IActionResult> RegisterLabtech([FromForm] labTechnicianModel user)
        {
            var labTech= await _labTechnicianManager.RegisterLabTech(user);
            return CreatedAtAction(nameof(GetLabtechById), new { id = labTech.Id }, labTech);
        }

        [HttpPut]
        [Route("{Id}", Name = "UpdateLabtech")]

        public async Task<IActionResult> UpdateLabtech(string id, [FromForm] labTechnicianModel user)
        {
            await _labTechnicianManager.UpdateLabTech(id, user);
            return Ok();
        }

        [HttpGet]
        [Route("{Id}", Name = "UpdateLabtech")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetLabtechById(string id)
        {
            var result = await _labTechnicianManager.GetLabTechById(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllLabtech")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllLabtech()
        {
            var result = await _labTechnicianManager.GetAllLabTechs();
            return Ok(result);
        }
    }
}
