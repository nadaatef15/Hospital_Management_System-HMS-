using Data.Entity;
using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.Doctor
{
    public interface IDoctorManager
    {
        Task Register(DoctorModel user);
        Task Update(string dctorId, ModifiedDoctor userModified);
    }
    public class DoctorManager : IDoctorManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        private readonly HMSDBContext _dbcontext;


        public DoctorManager(
            UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService, IUserManager userManager, HMSDBContext context)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
            _dbcontext = context;
        }

        public async Task Register(DoctorModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            var doctorEntity = user.ToDoctorEntity();

            if (user.Image is not null)
                doctorEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(doctorEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(doctorEntity, SysConstants.Doctor);
        }

        public async Task Update(string dctorId, ModifiedDoctor userModified)
        {
            var doctor = await _dbcontext.Doctors.FirstOrDefaultAsync(a => a.Id == dctorId) ??
                throw new NotFoundException(UseDoesnotExist);

            if (userModified.Id != dctorId)
                throw new ConflictException(NotTheSameId);

            doctor.Salary = userModified.Salary;
            await _userManager.UpdateUser(doctor, userModified);
        }


    }
}
