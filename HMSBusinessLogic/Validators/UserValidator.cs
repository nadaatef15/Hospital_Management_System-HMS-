using FluentValidation;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserModel> 
    {
        UserManager<UserEntity> _userManager;
        public UserValidator(UserManager<UserEntity> userManager)
        {
             _userManager = userManager;

            RuleFor(x => x.Email)
                .MustAsync(EmailNotTakenBefore)
                .WithMessage(EmailUsedBefore);

            RuleFor(x => x.UserName)
                .MustAsync(UserNameNotTakenBefore)
                .WithMessage(UserNameUsedBefore);
        }

        public async Task<bool> EmailNotTakenBefore(string email, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is null;
        }

        public async Task<bool> UserNameNotTakenBefore(string UserName, CancellationToken cancellation)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            return user is null;
        }
    }
}
