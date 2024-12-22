using HMSBusinessLogic.Manager.Appointment;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentManager _appointmentManager;
        public AppointmentController(IAppointmentManager appointmentManager) =>
            _appointmentManager = appointmentManager;


        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentModel model)
        {
            await _appointmentManager.CreateAppointment(model);
            return Ok();
        }


        [HttpDelete]
        [Route("{Id}", Name = "DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentManager.DeleteAppointment(id);
            return Ok();
        }

        [HttpGet]
        [Route("{Id}", Name = "GetAppointmentByIdAsNoTracking")]
        [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentManager.GetAppointmentById(id);
            return Ok(appointment);
        }

        [HttpGet("GetAllAppointments")]
        [ProducesResponseType(typeof(List<AppointmentResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointment = await _appointmentManager.GetAllAppointments();
            return Ok(appointment);
        }



    }
}
