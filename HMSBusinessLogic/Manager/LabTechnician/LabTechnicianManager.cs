using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;

namespace HMSBusinessLogic.Manager.LabTechnician
{
    public interface ILabTechnicianManager
    {
        Task Register(labTechnicianModel user);
        Task Update(string UserId, ModifyUser userModified);
    }
    public class LabTechnicianManager : ILabTechnicianManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;

        public LabTechnicianManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService, IUserManager userManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task Register(labTechnicianModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            var labTechnicialEntity = user.ToLabTechEntity();

            if (user.Image is not null)
                labTechnicialEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(labTechnicialEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(labTechnicialEntity, SysConstants.LabTechnician);
        }


        public async Task Update(string UserId, ModifyUser userModified)
        {
            await _userManager.UpdateUser(UserId, userModified);
        }
    }
}
