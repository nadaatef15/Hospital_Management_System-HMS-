using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Pharmacist;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.Pharmacist
{
    public interface IPharmacistManager
    {
        Task<UserResource> RegisterPharmacist(pharmacistModel user);
        Task UpdatePharmacist(string id, pharmacistModel pharmacistModel);
        Task<UserResource> GetPharmacistById(string id);
        Task<List<UserResource>> GetAllPharmacist();

    }
    public class PharmacistManager : IPharmacistManager
    {

        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        private readonly IPharmacistRepo _pharmacistRepo;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PharmacistManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService,
            IUserManager userManager, IPharmacistRepo pharmacistRepo, RoleManager<IdentityRole> roleManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
            _pharmacistRepo = pharmacistRepo;
            _roleManager = roleManager;
        }

        public async Task<List<UserResource>> GetAllPharmacist() =>
            (await _pharmacistRepo.GetAllPharmacist()).Select(a => a.ToResource()).ToList();

        public async Task<UserResource> GetPharmacistById(string id)
        {
            var Pharm = await _pharmacistRepo.GetPharmacistByIdAsNoTracking(id) ??
                throw new NotFoundException(pharmDoesnotExist);

            return Pharm.ToResource();
        }


        public async Task<UserResource> RegisterPharmacist(pharmacistModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            if (!await _roleManager.RoleExistsAsync(SysConstants.Pharmacist))
                throw new NotFoundException(RolePharmacistDoesNotExist);

            var PharmacistEntity = user.ToEntity();

            if (user.Image is not null)
                PharmacistEntity.ImagePath = await _fileService.UploadImage(user.Image);

            var result = await _userManagerIdentity.CreateAsync(PharmacistEntity, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(PharmacistEntity, SysConstants.Pharmacist);

            return PharmacistEntity.ToResource();

        }

        public async Task UpdatePharmacist(string id, pharmacistModel pharmacistModel)
        {
            await _validator.ValidateAndThrowAsync(pharmacistModel);

            if (pharmacistModel.Id != id)
                throw new ConflictException(NotTheSameId);

            var user = await _userManagerIdentity.FindByIdAsync(id) ??
                throw new NotFoundException(UseDoesnotExist);

            await _userManager.UpdateUser(user, pharmacistModel);
        }
    }
}
