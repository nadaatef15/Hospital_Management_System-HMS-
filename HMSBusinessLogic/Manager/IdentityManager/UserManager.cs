using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using HMSDataAccess.Reposatory.Identity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;


namespace HMSBusinessLogic.Manager.IdentityManager
{
    public interface IUserManager
    {
        Task AssignRolesToUser(string userId, List<string> rolesId);
        Task<UserResource> GetUserById(string userId);
        Task UpdateRolesForUser(string userId, List<string> rolesName);
        Task UpdateUser(string userId, ModifyUser userModel);
        Task DeleteUser(string userId);
        Task<List<UserResource>> GetAllUsers();

    }
    public class UserManager : IUserManager
    {
        IUserReposatory _userReposatory;
        RoleManager<IdentityRole> _roleManager;
        UserManager<UserEntity> _userManagerIdentity;
        private readonly IFileService _fileService;
        public UserManager(IUserReposatory userReposatory,
               UserManager<UserEntity> userManagerIdentity,
               RoleManager<IdentityRole> roleManager,
                IFileService fileService
              )
        {
            _userReposatory = userReposatory;
            _userManagerIdentity = userManagerIdentity;
            _roleManager = roleManager;
            _fileService = fileService;
          

        }

        public async Task AssignRolesToUser(string userId, List<string> roleNames)
        {
            var user = await _userManagerIdentity.FindByIdAsync(userId);
            if (user is not null)
            {
                foreach (var item in roleNames)
                {
                    var role = await _roleManager.FindByNameAsync(item);
                    if (role != null)
                    {
                        var hasRole = await _userManagerIdentity.IsInRoleAsync(user, item);
                        if (!hasRole)
                            await _userManagerIdentity.AddToRoleAsync(user, role.Name!);
                    }
                }
            }
        }

        public async Task<UserResource> GetUserById(string userId)
        {
            var user = await _userReposatory.GetUserById(userId);

            if (user is null)
                throw new NotFoundException("This user is not found");
            else
                return user.ToResource();

        }

        public async Task UpdateRolesForUser(string userId, List<string> rolesName)
        {
            var user = await _userManagerIdentity.FindByIdAsync(userId);
            if (user is not null)
            {
                var userRoles = await _userManagerIdentity.GetRolesAsync(user);
                userRoles.ToList().ForEach(async role => await _userManagerIdentity.RemoveFromRoleAsync(user, role));
                rolesName.ForEach(async role => await _userManagerIdentity.AddToRoleAsync(user, role));
            }
            else
                throw new NotFoundException("this User is not excest") ;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManagerIdentity.FindByIdAsync(userId);
            if (user is not null)
                await _userReposatory.DeleteUser(userId);
            else
                throw new NotFoundException("this User is not excest");
        }

        public async Task UpdateUser(string userId, ModifyUser userModified)
        {
           var user= await _userManagerIdentity.FindByIdAsync(userId);
            if (user is not null)
            {
                var name=await _userManagerIdentity.FindByNameAsync(userModified.UserName);
                if (name is null)
                {
                    user.PhoneNumber = userModified.Phone;
                    user.Address = userModified.Address;
                    user.Age = userModified.Age;
                    user.UserName = userModified.UserName;

                    if (userModified.Image is not null)
                        user.ImagePath = await _fileService.UploadImage(userModified.Image);
            
                }
                await _userReposatory.UpdateUser(user);
            }
            else
                throw new NotFoundException("this User is not excest");
        }

        public async Task<List<UserResource>> GetAllUsers()
        {
            var users = await _userReposatory.GetAllUsers();
            List<UserResource> usersList = new List<UserResource>();
            foreach (var user in users)
                usersList.Add(user.ToResource());

            return usersList;
        }

    }
}