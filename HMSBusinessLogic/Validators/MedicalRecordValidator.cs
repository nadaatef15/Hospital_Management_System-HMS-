using Azure.Core;
using Data.Entity;
using FluentValidation;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class MedicalRecordValidator : AbstractValidator<MedicalRecord>
    {
        private readonly HMSDBContext _dbcontext;
        public MedicalRecordValidator(HMSDBContext context)
        {
            _dbcontext = context;

            RuleFor(x => x.IsDeleted)
                .Equal(true)
                .WithMessage(MRDeleted);

        }

       
    }
}
