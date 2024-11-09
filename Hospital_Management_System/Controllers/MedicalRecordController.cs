using HMSBusinessLogic.Manager.MedicalRecord;
using HMSContracts.Model.MedicalRecord;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class MedicalRecordController : BaseController
    {
        private readonly IMedicalRecordManager _medicalRecordManager;
        public MedicalRecordController(IMedicalRecordManager medicalRecordManager)
        {
            _medicalRecordManager=medicalRecordManager;
        }

        [HttpPost("CreateMR")]
        public async Task<IActionResult> Create([FromBody] MedicalRecordModel model)
        {
           await _medicalRecordManager.CreateMR(model);
            return Ok();
        }

        [HttpDelete("DeleteMR")]
        public async Task<IActionResult> Delete(int id)
        {
            await _medicalRecordManager.Delete(id);
            return Ok();
        }


        [HttpPost("UpdateMR")]
        public async Task<IActionResult> Update(int id ,[FromBody] MedicalRecordModel model)
        {
            await _medicalRecordManager.Update(id,model);
            return Ok();
        }

        [HttpGet("GetMRById")]
        public async Task<IActionResult> GetById(int id)
        {
          var MR=  await _medicalRecordManager.GetById(id);
            return Ok(MR);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = _medicalRecordManager.GetAll();
            return Ok(result);
        }
    }
}
