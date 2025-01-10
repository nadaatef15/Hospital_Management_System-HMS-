using HMSBusinessLogic.Manager.MedicalRecord;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.MedicalRecord;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class MedicalRecordController : BaseController
    {
        private readonly IMedicalRecordManager _medicalRecordManager;
        public MedicalRecordController(IMedicalRecordManager medicalRecordManager) =>
            _medicalRecordManager = medicalRecordManager;


        [HttpPost(Name = "CreateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] MedicalRecordModel model)
        {
            await _medicalRecordManager.CreateMedicalRecord(model);
            return Created();
        }

        [HttpDelete("Id", Name = "DeleteMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            await _medicalRecordManager.DeleteMedicalRecord(id);
            return NoContent();
        }

        [HttpPost("Id", Name = "UpdateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMedicalRecord(int id ,[FromBody] MedicalRecordModel model)
        {
            await _medicalRecordManager.UpdateMedicalRecord(id,model);
            return NoContent();
        }

        [HttpGet("Id", Name = "GetMedicalRecordById")]
        [ProducesResponseType(typeof(MedicalRecordResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedicalRecordById(int id)
        {
          var medicalRecord =  await _medicalRecordManager.GetMedicalRecordById(id);
            return Ok(medicalRecord);
        }

        [HttpGet("GetAllMedicalRecords")]
        [ProducesResponseType(typeof(List<MedicalRecordResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var medicalRecords = _medicalRecordManager.GetAllMedicalRecords();
            return Ok(medicalRecords);
        }
    }
}
