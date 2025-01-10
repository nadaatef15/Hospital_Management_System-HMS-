using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.GeneralServices;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Receptionist;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;
namespace HMSBusinessLogic.Manager.Receptionist
{
    public interface IReceptionistManager
    {
        Task<UserResource> RegisterReceptionist(ReceptionistModel user);
        Task UpdateReceptionist(string id, ReceptionistModel userModified);
        Task<UserResource> GetReceptionistById(string id);
        Task<List<UserResource>> GetAllReceptionists();


    }
    public class ReceptionistManager : IReceptionistManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly IValidator<UserModel> _validator;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;
        private readonly IReceptionistRepo _receptionistRepo;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ReceptionistManager(UserManager<UserEntity> userManagerIdentity,
            IValidator<UserModel> validator, IFileService fileService,
            IUserManager userManager, IReceptionistRepo receptionistRepo, RoleManager<IdentityRole> roleManager)
        {
            _userManagerIdentity = userManagerIdentity;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
            _receptionistRepo = receptionistRepo;
            _roleManager = roleManager;
        }

        public async Task<UserResource> RegisterReceptionist(ReceptionistModel user)
        {
            await _validator.ValidateAndThrowAsync(user);

            if (! await _roleManager.RoleExistsAsync(SysConstants.Receptionist))
                throw new NotFoundException(RoleReceptionistDoesNotExist);

            var receptionistEntity = user.ToEntity();

            if (user.Image is not null)
                receptionistEntity.ImagePath = await _fileService.UploadImage(user.Image);

                var result = await _userManagerIdentity.CreateAsync(receptionistEntity, user.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, result.Errors);
                    throw new ValidationException(errors);
                }
                
            
            await _userManagerIdentity.AddToRoleAsync(receptionistEntity, SysConstants.Receptionist);
            
            return receptionistEntity.ToResource();

        }


        public async Task UpdateReceptionist(string id, ReceptionistModel userModified)
        {

            if (userModified.Id != id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(userModified);

            var user = await _userManagerIdentity.FindByIdAsync(id) ??
                 throw new NotFoundException(UseDoesnotExist);

            await _userManager.UpdateUser(user, userModified);
        }

        public async Task<UserResource> GetReceptionistById(string id)
        {
            var recep = await _receptionistRepo.GetReceptionistByIdAsNoTracking(id) ??
                throw new NotFoundException(UseDoesnotExist);

            return recep.ToResource();
        }

        public async Task<List<UserResource>> GetAllReceptionists() =>
            (await _receptionistRepo.GetAllReceptionist()).Select(a => a.ToResource()).ToList();


    }
}
