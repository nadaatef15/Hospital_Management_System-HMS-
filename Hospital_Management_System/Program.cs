using CloudinaryDotNet;
using Data.Entity;
using FluentValidation;
using HMSBusinessLogic.Manager.AccountManager;
using HMSBusinessLogic.Manager.Appointment;
using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Manager.LabTechnician;
using HMSBusinessLogic.Manager.MedicalRecord;
using HMSBusinessLogic.Manager.Patient;
using HMSBusinessLogic.Manager.PermissionManager;
using HMSBusinessLogic.Manager.Receptionist;
using HMSBusinessLogic.Seeds;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Validators;
using HMSContracts;
using HMSContracts.Email;
using HMSContracts.Model.Identity;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo;
using Hospital_Management_System.Refliction;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Hospital_Management_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Cloudinary
            var cloudinaryConfig = new CloudinaryDotNet.Account(
            builder.Configuration["Cloudinary:CloudName"],
            builder.Configuration["Cloudinary:ApiKey"],
            builder.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryConfig);
            builder.Services.AddSingleton(cloudinary);

            //Localization
            var localizationOptions = new RequestLocalizationOptions();
            var supportCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ar")
            };

            localizationOptions.SupportedCultures = supportCultures;
            localizationOptions.SupportedUICultures = supportCultures;
            localizationOptions.SetDefaultCulture("en");
            localizationOptions.ApplyCurrentCultureToResponseHeaders = true;





            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
            builder.Services.DBContextService(builder.Configuration);
            builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<HMSDBContext>();
            builder.Services.AuthenticationService(builder.Configuration);
            builder.Services.SwaggerConfiguration();
            builder.Services.AddLocalization();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IRoleManager, RoleManager>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IPermission, PermissionManager>();
            builder.Services.AddScoped<IValidator<UserModel>, UserValidator>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddTransient<IEmail, EmailSender>();
            builder.Services.AddScoped<IReceptionistManager, ReceptionistManager>();
            builder.Services.AddScoped<IReceptionistRepo, ReceptionistRepo>();
            builder.Services.AddScoped<IDoctorManager, DoctorManager>();
            builder.Services.AddScoped<IPatientsManager, PatientsManager>();
            builder.Services.AddScoped<ILabTechnicianManager, LabTechnicianManager>();
            builder.Services.AddScoped<IMedicalRecordREpo, MedicalRecordRepo>();
            builder.Services.AddScoped<IMedicalRecordManager, MedicalRecordManager>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddScoped<IAppointmentManager, AppointmentManager>();



            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");

                try
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                    logger.LogInformation("Data SEEDED");
                    logger.LogInformation("Application started");
                    // Seed Admin
                    await SeedRoleAdmin.SeedAdminRole(roleManager);
                    await SeedUserAdmin.SeedAdmin(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "There is an error");
                }
            }

            // Configure the HTTP request pipeline.
            app.UseRequestLocalization(localizationOptions);
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //   app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
