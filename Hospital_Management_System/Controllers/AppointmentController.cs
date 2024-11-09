using HMSBusinessLogic.Manager.Appointment;
using HMSContracts.Model.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentManager _appointmentManager;
        public AppointmentController(IAppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAppointment([FromBody]AppointmentModel model)
        {
            await _appointmentManager.CreateAppointment(model);
            return Ok();
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentManager.Delete(id);
            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var apoointment = await _appointmentManager.GetById(id);
            return Ok(apoointment);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var appointment = _appointmentManager.GetAll();
            return Ok(appointment);
        }



    }
}
