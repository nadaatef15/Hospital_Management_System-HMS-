using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class PharmacistMapping
    {
        public static PharmacistEntity ToEntity(this pharmacistModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.Phone,
            Age = user.Age,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };
    }
}
