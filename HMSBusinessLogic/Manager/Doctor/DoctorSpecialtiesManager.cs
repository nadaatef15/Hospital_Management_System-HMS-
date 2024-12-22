using FluentValidation;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Doctor;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Manager.Doctor
{
    public interface IDoctorSpecialtiesManager
    {
        Task AddDoctorSpeciality(DoctorSpecialties doctorSpecialty);
        Task DeleteDoctorSpecialty(DoctorSpecialties doctorSpecialty);
        Task<List<DoctorSpecialties>> GetDoctorSpecialties(string docId);

    }
    public class DoctorSpecialtiesManager : IDoctorSpecialtiesManager
    {
        private readonly IDoctorSpecialtiesRepo _doctorSpecialtiesRepo;
        private readonly IValidator<DoctorSpecialties> _validator;
        public DoctorSpecialtiesManager(IDoctorSpecialtiesRepo repo, IValidator<DoctorSpecialties> validator)
        {
            _doctorSpecialtiesRepo = repo;
            _validator = validator;
        }

        public async Task AddDoctorSpeciality(DoctorSpecialties doctorSpecialty)
        {
            await _validator.ValidateAndThrowAsync(doctorSpecialty);

            var docSpecialty = await _doctorSpecialtiesRepo.DoctorHasSpecialty(doctorSpecialty);

            if (docSpecialty is not null)
                throw new ConflictException(DocHasSpecialty);
       
            _doctorSpecialtiesRepo.AddSpecialtyToDoctor(doctorSpecialty);
        }

        public async Task DeleteDoctorSpecialty(DoctorSpecialties doctorSpecialty)
        {
            await _validator.ValidateAndThrowAsync(doctorSpecialty);

            _doctorSpecialtiesRepo.DeleteSpecialtyOfDoctor(doctorSpecialty);
        }

        public async Task<List<DoctorSpecialties>> GetDoctorSpecialties(string docId)
        {
            var docSpecialty = await _doctorSpecialtiesRepo.GetDoctorSpecialties(docId) ??
                  throw new NotFoundException(NoSpecialtyForDoctor);

            return docSpecialty;
        }
    }
}
