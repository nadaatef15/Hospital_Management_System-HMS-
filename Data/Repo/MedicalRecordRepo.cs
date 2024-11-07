using Data.Entity;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSDataAccess.Repo
{
    public interface IMedicalRecordREpo
    {
        Task CreateMedicalRecord(MedicalRecord model);
        Task Delete(int MRId);
        Task Update(int MRId, MedicalRecord model);
        Task<MedicalRecord> GetById(int id);
        List<MedicalRecord> GetAll();
    }
    public class MedicalRecordRepo : IMedicalRecordREpo
    {
        private readonly HMSDBContext _dbcontext;

        public MedicalRecordRepo(HMSDBContext context)
        {
            _dbcontext = context;
        }

        public async Task CreateMedicalRecord(MedicalRecord model)
        {
            var appointment = await _dbcontext.Appointments.FirstOrDefaultAsync(a => a.Id == model.AppointmentId) ??
                throw new NotFoundException(appointmentDoesnotExist);

            await _dbcontext.MedicalRecord.AddAsync(model);
            await Reposatory.SaveAsync(_dbcontext);
        }

        public async Task Delete(int MRId)
        {
            var target = await _dbcontext.MedicalRecord.FirstOrDefaultAsync(a => a.Id == MRId) ??
                throw new NotFoundException(MedicalRecordDoesnotExist);

            target.IsDeleted = true;
            await Reposatory.SaveAsync(_dbcontext);
        }

        public async Task Update(int MRId, MedicalRecord model)
        {

            var target = await _dbcontext.MedicalRecord.FirstOrDefaultAsync(a => a.Id == MRId) ??
                throw new NotFoundException(MedicalRecordDoesnotExist);

            if (target.IsDeleted)
                throw new ConflictException(MRDeleted);

            var appointment = await _dbcontext.Appointments.Where(a => a.PatientId == model.PatientId)
                             .FirstOrDefaultAsync(a => a.Id == model.AppointmentId) ??
                throw new ConflictException(patientDoesnotHasThisAppointment);

            _dbcontext.MedicalRecord.Update(model);
            await Reposatory.SaveAsync(_dbcontext);
        }


        public async Task<MedicalRecord> GetById(int id)
        {
            var MR = await _dbcontext.MedicalRecord.FirstOrDefaultAsync(a => a.Id == id) ??
                  throw new NotFoundException(MedicalRecordDoesnotExist);
            return MR;
        }


        public List<MedicalRecord> GetAll() =>
             _dbcontext.MedicalRecord.ToList();

    }
}
