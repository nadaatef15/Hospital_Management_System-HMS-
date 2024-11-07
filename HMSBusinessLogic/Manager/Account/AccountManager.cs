﻿using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HMSContracts.Constants.SysConstants;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.AccountManager
{
    public interface IAccountManager
    {
        Task<JwtSecurityToken> Login(LoginModel loginModel);
        Task Register(UserModel user);

        Task ChangePassword(string userId, [FromBody] ChangePasswordModel model);
    }
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;

        public AccountManager(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
             IValidator<UserModel> validator,
             IFileService fileService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _validator = validator;
            _fileService = fileService;
        }

        public async Task<JwtSecurityToken> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
                throw new ConflictException(EmailNotFound);

            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result)
                throw new ConflictException(WrongPassword);


            var claims = new List<Claim>()
            {
                new (ClaimTypes.Name ,user.UserName!),
                new (ClaimTypes.NameIdentifier ,user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

                var identityRole = await _roleManager.FindByNameAsync(role);

                var permissions = (await _roleManager.GetClaimsAsync(identityRole!)).Where(x => x.Type == Permission);

                foreach (var item in permissions)
                    claims.Add(new Claim(Permission, item.Value));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                 issuer: _configuration["Jwt:Issuer"],
                 audience: _configuration["Jwt:Audience"],
                 expires: DateTime.Now.AddHours(2),
                 claims: claims,
                 signingCredentials: signingCredentials);

            return Token;
        }

        public async Task Register(UserModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            var userEntity = user.ToEntity();

            if (user.Image is not null)
                userEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManager.CreateAsync(userEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }
        }


        public async Task ChangePassword(string userId, [FromBody] ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new NotFoundException(UseDoesnotExist);

            var isCurrentPassword = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!isCurrentPassword)
                throw new ConflictException(WrongPassword);

            await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }


    }
}
