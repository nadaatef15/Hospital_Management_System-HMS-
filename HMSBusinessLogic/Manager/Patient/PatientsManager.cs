using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Patient
{
    public interface IPatientsManager
    {
        Task Register(PatientModel user);
        Task Update(string UsertId, UserModel userModified);
    }
    public class PatientsManager : IPatientsManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        public PatientsManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService,
            IUserManager userManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task Register(PatientModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            var PatientEntity = user.ToPatientEntity();

            if (user.Image is not null)
                PatientEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(PatientEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(PatientEntity, SysConstants.Patient);
        }

        public async Task Update(string id, UserModel userModified)
        {

            if (userModified.Id != id)
                throw new ConflictException(NotTheSameId);

            var user = await _userManagerIdentity.FindByIdAsync(id) ??
                 throw new NotFoundException(UseDoesnotExist);

            await _userManager.UpdateUser(user, userModified);
        }
    }
}
