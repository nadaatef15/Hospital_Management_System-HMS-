using HMSContracts.Model.Identity;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Services.user
{
    public interface IUserService
    {
        void SetValues(UserEntity entity, UserModel model);
    }
    public class UserService : IUserService
    {
        public void SetValues(UserEntity entity, UserModel model)
        {
            entity.PhoneNumber = model.Phone;
            entity.Address = model.Address;
            entity.Age = model.Age;
            entity.UserName = model.UserName;
            entity.Email = model.Email;
        }
    }
}
