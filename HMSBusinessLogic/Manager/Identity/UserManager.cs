using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Services.user;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Identity
{
    public interface IUserManager
    {
        Task AssignRolesToUser(string userId, List<string> rolesId);
        Task<UserResource> GetUserById(string userId);
        Task UpdateUser(UserEntity user, UserModel userModified);
        Task DeleteUser(string userId);
        Task<List<UserResource>> GetAllUsers();

    }
    public class UserManager : IUserManager
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileService _fileService;
        private readonly IUserService _userEntitiesUpdateService;

        public UserManager(UserManager<UserEntity> userManagerIdentity,
            IFileService fileService,
            IUserService userEntitiesUpdateService
           )
        {
            _userManager = userManagerIdentity;
            _fileService = fileService;
            _userEntitiesUpdateService = userEntitiesUpdateService;
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

            if (user is null || user.IsDeleted == true)
                throw new NotFoundException(UseDoesnotExist);

            return user.ToResource();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)??
                   throw new NotFoundException(UseDoesnotExist);

            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateUser(UserEntity user, UserModel userModified)
        {
            _userEntitiesUpdateService.SetValues(user, userModified);   

            if (userModified.Image is not null)
                user.ImagePath = await _fileService.UploadImage(userModified.Image);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var messages = string.Join(", \n", result.Errors);
                throw new ConflictException(messages);
            }
        }

        public async Task<List<UserResource>> GetAllUsers()=>
             await _userManager.Users.Select(x => x.ToResource()).ToListAsync();
        



    }
}