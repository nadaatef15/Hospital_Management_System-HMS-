using CloudinaryDotNet;
using Data.Entity;
using FluentValidation;
using HMSBusinessLogic.Manager.AccountManager;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Manager.PermissionManager;
using HMSBusinessLogic.Seeds;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Validators;
using HMSContracts.Infrastructure.Exceptions;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using HMSDataAccess.Reposatory.Account;
using HMSDataAccess.Reposatory.Identity;
using Hospital_Management_System.Refliction;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cloudinaryConfig = new CloudinaryDotNet.Account(
            builder.Configuration["Cloudinary:CloudName"],
            builder.Configuration["Cloudinary:ApiKey"],
            builder.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryConfig);
            builder.Services.AddSingleton(cloudinary);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.DBContextService(builder.Configuration);
            builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<HMSDBContext>();
            builder.Services.AuthenticationService(builder.Configuration);
            builder.Services.SwaggerConfiguration();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped<IUserReposatory, UserReposatory>();
            builder.Services.AddScoped<IRoleReposatory, RoleReposatory>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IRoleManager, RoleManager>();
            builder.Services.AddScoped<IAccountReposatory, AccountReposatory>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IPermission, PermissionManager>();
            builder.Services.AddScoped<IValidator<UserModel>, UserValidator>();
            builder.Services.AddScoped<IFileService, FileService>();

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
