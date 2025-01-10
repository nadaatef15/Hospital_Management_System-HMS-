using FluentValidation;
using HMSContracts.Model.Appointment;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentModel>
    {
        private readonly HMSDBContext _dbcontext;
        public AppointmentValidator(HMSDBContext dbcontext)
        {
            _dbcontext= dbcontext;

            RuleFor(x => x)
              .MustAsync(IsDoctorExist)
              .WithMessage(UseDoesnotExist);

            RuleFor(x => x)
                .MustAsync(IsPatientExist)
                .WithMessage(UseDoesnotExist);
        }


        public async Task<bool> IsDoctorExist(AppointmentModel model, CancellationToken cancellation)
        {
            var doctor = await _dbcontext.Doctors.FirstOrDefaultAsync(a => a.Id == model.DoctorId);
            return doctor is null;
        }

        public async Task<bool> IsPatientExist(AppointmentModel model, CancellationToken cancellation)
        {
            var patient = await _dbcontext.Patients.FirstOrDefaultAsync(a => a.Id == model.PatientId);
            return patient is null;
        }

    }
}
