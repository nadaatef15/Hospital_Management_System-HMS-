using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.MedicalRecord;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.MedicalRecord;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.MedicalRecord
{
    public interface IMedicalRecordManager
    {
        Task CreateMedicalRecord(MedicalRecordModel model);
        Task DeleteMedicalRecord(int id);
        Task UpdateMedicalRecord(int Id, MedicalRecordModel model);
        Task<MedicalRecordResource> GetMedicalRecordById(int id);
        Task<List<MedicalRecordResource>> GetAllMedicalRecords();
    }
    public class MedicalRecordManager : IMedicalRecordManager
    {
        private readonly IMedicalRecordREpo _medicalRecordRepo;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IValidator<MedicalRecordModel> _validator;
        private readonly IMedicalRecordService _medicalRecordUpdateService;

        public MedicalRecordManager(IMedicalRecordREpo medicalRecordRepo,
            UserManager<UserEntity> userManager ,
            IValidator<MedicalRecordModel> validators ,
            IMedicalRecordService medicalRecordUpdateService
            )
        {
            _medicalRecordRepo = medicalRecordRepo;
            _userManager = userManager;
            _validator = validators;
            _medicalRecordUpdateService = medicalRecordUpdateService;
        }


        public async Task CreateMedicalRecord(MedicalRecordModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var medicalRecord = model.ToEntity();

            await _medicalRecordRepo.CreateMedicalRecord(medicalRecord);
        }

        public async Task DeleteMedicalRecord(int id)
        {
           var result= await _medicalRecordRepo.GetMedicalRecordByIdNoTracking(id) ??
                  throw new NotFoundException(MedicalRecordDoesnotExist);

            await _medicalRecordRepo.DeleteMedicalRecord(result);
        }

        public async Task UpdateMedicalRecord(int id, MedicalRecordModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var medicalRecordEntity = await _medicalRecordRepo.GetMedicalRecordByIdTracking(id) ??
                   throw new NotFoundException(MedicalRecordDoesnotExist);

            _medicalRecordUpdateService.SetValues(medicalRecordEntity, model);

            await _medicalRecordRepo.SaveChanges();
        }

        public async Task UpdateMedicalRecord2(int id, MedicalRecordModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var medicalRecordEntity = await _medicalRecordRepo.GetMedicalRecordByIdNoTracking(id) ??
                   throw new NotFoundException(MedicalRecordDoesnotExist);

            _medicalRecordUpdateService.SetValues(medicalRecordEntity, model);

            await _medicalRecordRepo.UpdateMedicalRecord(medicalRecordEntity);
        }

        public async Task<MedicalRecordResource> GetMedicalRecordById(int id)
        {
            var medicalRecord = await _medicalRecordRepo.GetMedicalRecordByIdNoTracking(id) ??
                throw new NotFoundException(MedicalRecordDoesnotExist);

            return medicalRecord.ToResource();        
        }

        public async Task< List<MedicalRecordResource>> GetAllMedicalRecords()=>
            (await _medicalRecordRepo.GetAllMedicalRecords()).Select(a =>a.ToResource()).ToList();

    }

}
