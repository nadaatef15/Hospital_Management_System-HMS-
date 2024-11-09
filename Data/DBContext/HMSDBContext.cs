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

        public HMSDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<ReceptionistEntity> Receptionists { get; set; }
        public DbSet<PharmasistEntity> Pharmasists { get; set; }
        public DbSet<LabTechnicianEntity> LabTechnicians { get; set; }
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

            builder.Entity<DoctorEntity>()
                .ToTable("Doctors");

            builder.Entity<PatientEntity>()
                .ToTable("Patients");

            builder.Entity<ReceptionistEntity>()
              .ToTable("Receptionists");

            builder.Entity<PharmasistEntity>()
              .ToTable("Pharmasists");

            builder.Entity<LabTechnicianEntity>()
              .ToTable("LabTechnicians");



        }
    }
}
