using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;

namespace HMSDataAccess.Repo
{
    public interface IReceptionistRepo
    {

    }
    public class ReceptionistRepo :IReceptionistRepo
    {
        UserManager<UserEntity> _userManager;
        public ReceptionistRepo(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;   
        }
           

    }
}
