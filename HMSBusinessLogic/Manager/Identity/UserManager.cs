using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.IdentityManager
{
    public interface IUserManager
    {
        Task AssignRolesToUser(string userId, List<string> rolesId);
        Task<UserResource> GetUserById(string userId);
        Task UpdateUser(string userId, ModifyUser userModel);
        Task DeleteUser(string userId);
        Task<List<UserResource>> GetAllUsers();

    }
    public class UserManager : IUserManager
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileService _fileService;

        public UserManager(UserManager<UserEntity> userManagerIdentity,
            IFileService fileService
           )
        {
            _userManager = userManagerIdentity;
            _fileService = fileService;
        }

        public async Task AssignRolesToUser(string userId, List<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            var currentUserRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = roleNames.Except(currentUserRoles).ToList();
            var rolesToRemove = currentUserRoles.Except(roleNames).ToList();

            foreach (var item in rolesToAdd)
                await _userManager.AddToRoleAsync(user, item);

            foreach (var role in rolesToRemove)
                await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<UserResource> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null || user.isDeleted == true)
                throw new NotFoundException(UseDoesnotExist);
           
            return user.ToResource();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            //await _userManagerIdentity.DeleteAsync(user);
            user.isDeleted = true;
        }

        public async Task UpdateUser(string userId, ModifyUser userModified)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            //if the the email or the name is used before in the application

            var userByEmail = await _userManager.FindByEmailAsync(userModified.Email);
            var userByUserName = await _userManager.FindByNameAsync(userModified.UserName);

            if (userByUserName.Id != user.Id || userByEmail.Id != user.Id)
                throw new ConflictException(UsedBefore);

            else
            {
                user.PhoneNumber = userModified.Phone;
                user.Address = userModified.Address;
                user.Age = userModified.Age;
                user.UserName = userModified.UserName;
                user.Email = userModified.Email;

                if (userModified.Image is not null)
                    user.ImagePath = await _fileService.UploadImage(userModified.Image);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var messages = string.Join(", \n", result.Errors);
                throw new ConflictException(messages);
            }
        }

        public async Task<List<UserResource>> GetAllUsers()
        {

            var users = await _userManager.Users.ToListAsync();

            return users.Where(a => a.isDeleted == false).Select(x => x.ToResource()).ToList();
        }



    }
}