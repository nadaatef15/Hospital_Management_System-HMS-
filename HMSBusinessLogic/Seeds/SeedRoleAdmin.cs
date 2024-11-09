using HMSContracts.Constants;
using Microsoft.AspNetCore.Identity;

namespace HMSBusinessLogic.Seeds
{
    public static class SeedRoleAdmin
    {
        public static async Task SeedAdminRole(RoleManager<IdentityRole> roleManager)
        {
           IdentityRole result= await roleManager.FindByNameAsync(SysConstants.Admin);
            if (result is null)
            {
                IdentityRole role = new()
                {
                    Name = SysConstants.Admin
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
