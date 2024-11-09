using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.AccountManager;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Manager.Receptionist
{
    public interface IReceptionistManager
    {
        Task Register(ReceptionistModel user);
        Task Update(string id, UserModel userModified);

    }
    public class ReceptionistManager : IReceptionistManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;

        public ReceptionistManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService , IUserManager userManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task Register(ReceptionistModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            var reseptionistEntity = user.ToReceptionistEntity();

            if (user.Image is not null)
                reseptionistEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(reseptionistEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }
            await _userManagerIdentity.AddToRoleAsync(reseptionistEntity, SysConstants.Receptionist);

        }

        #region Update
        //public async Task Update(string ReceptionistId, ModifyUser userModified)
        //{
        //    var receptionist = await _userManagerIdentity.FindByIdAsync(ReceptionistId);

        //    if (receptionist is null)
        //        throw new NotFoundException(UseDoesnotExist);


        //    //if the the email or the name is used before in the application

        //    var userByEmail = await _userManagerIdentity.FindByEmailAsync(userModified.Email);
        //    var userByUserName = await _userManagerIdentity.FindByNameAsync(userModified.UserName);

        //    if (userByUserName.Id != receptionist.Id || userByEmail.Id != receptionist.Id)
        //        throw new ConflictException("The UserName or Email is used before");

        //    else
        //    {
        //        receptionist.PhoneNumber = userModified.Phone;
        //        receptionist.Address = userModified.Address;
        //        receptionist.Age = userModified.Age;
        //        receptionist.UserName = userModified.UserName;
        //        receptionist.Email = userModified.Email;

        //        if (userModified.Image is not null)
        //            receptionist.ImagePath = await _fileService.UploadImage(userModified.Image);
        //    }
        //    var result = await _userManagerIdentity.UpdateAsync(receptionist);

        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(Environment.NewLine, result.Errors);
        //        throw new ValidationException(errors);
        //    }
        //}
        #endregion
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
