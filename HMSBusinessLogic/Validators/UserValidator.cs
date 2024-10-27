using FluentValidation;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;

namespace HMSBusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        UserManager<UserEntity> userManager;
        public UserValidator(UserManager<UserEntity> _userManager)
        {
            userManager = _userManager;

            RuleFor(x => x.Email).MustAsync(EmailTokenBefore).WithMessage("This Email is used before");
            RuleFor(x => x.UserName).MustAsync(UserNameTokenBefore).WithMessage("This User Name is used before");
        }

        public async Task<bool> EmailTokenBefore(string email, CancellationToken cancellation)
        {
            var user= await userManager.FindByEmailAsync(email);
            if (user is null) return true;
            return false; 
        }

        public async Task<bool> UserNameTokenBefore(string UserName, CancellationToken cancellation)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user is null) return true;
            return false;
        }



    }
}
