using HMSBusinessLogic.Helpers.Mappers;
using HMSContracts.Model.Appointment;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.MedicalRecord
{
    public interface IMedicalRecordManager
    {
        Task CreateMR(MedicalRecordModel model);
        Task Delete(int id);
        Task Update(int MRId, MedicalRecordModel model);
        Task<MedicalRecordModel> GetById(int id);
        List<MedicalRecordModel> GetAll();

    }
    public class MedicalRecordManager : IMedicalRecordManager
    {
        private readonly IMedicalRecordREpo _medicalRecordRepo;
        private readonly UserManager<UserEntity> _userManager;

        public MedicalRecordManager(IMedicalRecordREpo medicalRecordRepo,
            UserManager<UserEntity> userManager

            )
        {
            _medicalRecordRepo = medicalRecordRepo;
            _userManager = userManager;
        }


        public async Task CreateMR(MedicalRecordModel model)
        {
            var doc = await _userManager.FindByIdAsync(model.DoctorId);
            var patient = await _userManager.FindByIdAsync(model.PatientId);

            if (doc is null || patient is null)
                throw new NotFoundException(UseDoesnotExist);

            var medicalRecord = model.ToMedicalRecord();

            await _medicalRecordRepo.CreateMedicalRecord(medicalRecord);
        }

        public async Task Delete(int id)
        {
            await _medicalRecordRepo.Delete(id);
        }

        public async Task Update(int MRId, MedicalRecordModel model)
        {

            var doc = await _userManager.FindByIdAsync(model.DoctorId);
            var patient = await _userManager.FindByIdAsync(model.PatientId);

            if (doc is null || patient is null)
                throw new NotFoundException(UseDoesnotExist);

            var medicalRecord = model.ToMedicalRecord();

            await _medicalRecordRepo.Update(MRId, medicalRecord);
        }

        public async Task<MedicalRecordModel> GetById(int id)
        {
            var MR = await _medicalRecordRepo.GetById(id);
            return MR.ToMRModel();        
        }


        public List<MedicalRecordModel> GetAll()
        {
            var result = _medicalRecordRepo.GetAll();
            List<MedicalRecordModel> models = new();
            result.ForEach(a => models.Add(a.ToMRModel()));
            return models;
        }
    }

}
