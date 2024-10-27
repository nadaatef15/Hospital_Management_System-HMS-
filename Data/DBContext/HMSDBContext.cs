using HMSDataAccess.Entity;
using HMSDataAccess.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Entity
{
    public class HMSDBContext : IdentityDbContext<UserEntity>
    {
        public HMSDBContext()
        {
            
        }

        public HMSDBContext(DbContextOptions options):base(options)
        {
           
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Pharmasist> Pharmasists { get; set; }
        public DbSet<LabTechnician> LabTechnicians { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Diagnoses> Diagnoses { get; set; }
        public DbSet<MedicalRecordDiagnoses> MedicalRecordDiagnoses { get; set; }
        public DbSet<MedicalRecordTests> MedicalRecordTests { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<DoctorSpecialties> DoctorSpecialties { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedule { get; set; }
        public DbSet<Specialties> Specialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
