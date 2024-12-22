using HMSBusinessLogic.Manager.Doctor;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorSpcialtiesController : BaseController
    {
        private readonly IDoctorSpecialtiesManager _doctorSpecialtiesManager;
        public DoctorSpcialtiesController(IDoctorSpecialtiesManager doctorSpecialtiesManager)=>
            _doctorSpecialtiesManager = doctorSpecialtiesManager;

        [HttpPost(Name = "AddDoctorSpecialty")]
        [ProducesResponseType(typeof(DoctorSpecialties),StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddDoctorSpecialty(DoctorSpecialties doctorSpecialties)
        {
           await _doctorSpecialtiesManager.AddDoctorSpeciality(doctorSpecialties);
            return Ok(); ;
        }

        [HttpDelete(Name = "DeleteDoctorSpecialty")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDoctorSpeciality(DoctorSpecialties doctorSpecialty)
        {
            await _doctorSpecialtiesManager.DeleteDoctorSpecialty(doctorSpecialty);
            return NoContent(); 
        }

        [HttpGet(Name ="DoctorHasSpecialty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorSpecialty(string docId)
        {
           var docSpecialty= await _doctorSpecialtiesManager.GetDoctorSpecialties(docId);
            return Ok(docSpecialty);
        }

    }
}
