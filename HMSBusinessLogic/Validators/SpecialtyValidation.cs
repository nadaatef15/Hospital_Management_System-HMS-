using FluentValidation;
using HMSContracts.Model.Specialty;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class SpecialtyValidation :AbstractValidator<SpecialtyModel>
    {
        private readonly HMSDBContext _dbContext;
        public SpecialtyValidation(HMSDBContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x)
                .MustAsync(NameOfSpecialtyIsTaken)
                .WithMessage(SpecialityIsExist);

            RuleFor(x => x.Name)
                .Must(ValidNameOfSpecialty)
                .WithMessage(NameIsNotCorrect);
        }

        public bool ValidNameOfSpecialty(string specialtyName )
        {
            if (string.IsNullOrWhiteSpace(specialtyName) || specialtyName.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }
        public async Task<bool> NameOfSpecialtyIsTaken(SpecialtyModel specialty, CancellationToken cancellationToken)
        {
            var name = await _dbContext.Specialties.AnyAsync(a=>a.Name== specialty.Name);
            return name is false ;
        }
    }
}
