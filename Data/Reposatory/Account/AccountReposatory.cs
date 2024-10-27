using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;

namespace HMSDataAccess.Reposatory.Account
{
    public interface IAccountReposatory
    {
        Task<UserEntity> Login(string username, string password);
    }
    public class AccountReposatory : IAccountReposatory
    {
        UserManager<UserEntity> userManager;

        public AccountReposatory(UserManager<UserEntity> _userManager)
        {
            userManager = _userManager;
        }
        public async Task<UserEntity> Login(string Email, string password)
        {
            var user = await userManager.FindByEmailAsync(Email);

            if (user is null)
                throw new ConflictException("This Email does not excest");

            var result = await userManager.CheckPasswordAsync(user, password);
            if (!result)
                throw new ConflictException("The password is not Correct");

            return user;
        }

    }
}
