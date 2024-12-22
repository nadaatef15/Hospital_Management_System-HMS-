using FluentValidation;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Validators
{
    public class MedicalRecordValidator : AbstractValidator<MedicalRecordModel>
    {
        private readonly HMSDBContext _dbContext;
        public MedicalRecordValidator(HMSDBContext context)
        {
            _dbContext = context;

            RuleFor(x => x)
                .MustAsync(DoctorExist)
                .WithMessage(UseDoesnotExist);

            RuleFor(x => x)
                .MustAsync(PatientExist)
                .WithMessage(UseDoesnotExist);


            RuleFor(x => x)
               .MustAsync(PatientHasTheAppointment)
               .WithMessage(patientDoesnotHasThisAppointment);
        }

        public async Task<bool> DoctorExist(MedicalRecordModel model, CancellationToken cancellation)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(a => a.Id == model.DoctorId);
            return doctor is null;
        }

        public async Task<bool> PatientExist(MedicalRecordModel model, CancellationToken cancellation)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(a => a.Id == model.PatientId);
            return patient is null;
        }

        public async Task<bool> PatientHasTheAppointment(MedicalRecordModel model, CancellationToken cancellation)
        {
            var appointment = await _dbContext.Appointments.Where(a => a.PatientId == model.PatientId).FirstOrDefaultAsync(a => a.Id == model.AppointmentId);
            return appointment is null;
        }

    }
}
