using HMSBusinessLogic.Resource;
using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class UserMapping
    {
        public static UserEntity ToEntity(this UserModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };

        public static UserResource ToResource(this UserEntity user) => new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Phone = user.PhoneNumber,
            Email = user.Email,
            Age = user.Age,
            Address = user.Address,
            Image = user.ImagePath,
            Gender = user.Gender.ToString(),

        };
    }

}
