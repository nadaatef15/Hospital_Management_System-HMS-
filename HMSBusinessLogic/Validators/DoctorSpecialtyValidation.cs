using FluentValidation;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class DoctorSpecialtyValidation : AbstractValidator<DoctorSpecialties>
    {
        private readonly HMSDBContext _dbContext;
        public DoctorSpecialtyValidation( HMSDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(a => a)
                .MustAsync(IsDoctorIdvalid)
                .WithMessage(UseDoesnotExist);

            RuleFor(a => a)
               .MustAsync(IsSpecialtyIdvalid)
               .WithMessage(SpecialityIsNotExist);

            //RuleFor(a => a)
            //    .MustAsync(IsDoctorSpecialtyExist)
            //    .WithMessage(DocHasSpecialty);

        }

        public async Task< bool> IsDoctorIdvalid(DoctorSpecialties doctorSpecialty , CancellationToken cancellationToken)
        {
            var doctor =await _dbContext.Doctors.FirstOrDefaultAsync(a=>a.Id == doctorSpecialty.DoctorId);
            return doctor is not null;
        }

        public async Task<bool> IsSpecialtyIdvalid(DoctorSpecialties doctorSpecialty, CancellationToken cancellationToken)
        {
            var specialty = await _dbContext.Specialties.FirstOrDefaultAsync(a=>a.Id == doctorSpecialty.SpecialtyId);
            return specialty is not null;
        }

        //public async Task<bool> IsDoctorSpecialtyExist(DoctorSpecialties DoctorSpecialties, CancellationToken cancellationToken)
        //{
        //    var doctorSpecialty = await _dbContext.DoctorSpecialties
        //        .FirstOrDefaultAsync(a=>a.DoctorId == DoctorSpecialties.DoctorId && 
        //                    a.SpecialtyId ==DoctorSpecialties.SpecialtyId);

        //    return doctorSpecialty is null;
        //}
    }
}
