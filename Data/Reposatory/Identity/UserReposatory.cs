using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;

namespace HMSDataAccess.Reposatory.Identity
{
    public interface IUserReposatory
    {
        Task DeleteUser(string userId);
        Task<UserEntity> GetUserById(string userId);
        Task UpdateUser(UserEntity user);
        Task<IQueryable<UserEntity>> GetAllUsers();
        Task<List<string>> GetRolesOfUser(string userId);

    }
    public class UserReposatory : IUserReposatory
    {
        UserManager<UserEntity> userManager;
        public UserReposatory(UserManager<UserEntity> _userManager)
        {
            userManager = _userManager;
        }

        public async Task UpdateUser(UserEntity user)
        {
           var result=  await userManager.UpdateAsync(user);
            if(!result.Succeeded) { 
             foreach(var error in result.Errors)
                {
                    error.Description.ToString();
                }
            }
        }

        public async Task DeleteUser(string userId)
        {
            var user = await GetUserById(userId);
            await userManager.DeleteAsync(user);
        }

        public async Task<UserEntity> GetUserById(string userId)
        {
          var user=  await userManager.FindByIdAsync(userId);
            return user;
        }

        public Task<IQueryable<UserEntity>> GetAllUsers() =>
             Task.FromResult(userManager.Users);

       

        public async Task<List<string>> GetRolesOfUser(string userId)
        {
            var user = await GetUserById(userId);   
            var result= await userManager.GetRolesAsync(user);
            return result.ToList();
        }
    }
}
