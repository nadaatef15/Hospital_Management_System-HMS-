using HMSBusinessLogic.ManagePermissions;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static HMSContracts.Constants.SysConstants;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Seeds
{
    public static class SeedUserAdmin
    {
        public static async Task SeedAdmin(UserManager<UserEntity> userManager , RoleManager<IdentityRole> roleManager)
        {
            var admin = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin.123@gmail.com",
                Address = "Kafr",
                Age = 25,
                Gender = Gender.M,
                ImagePath = "1.jpg",

            };

            UserEntity result = await userManager.FindByEmailAsync(admin.Email);
            if (result == null)
            {
                await userManager.CreateAsync(admin, "Admin123@");
                await userManager.AddToRoleAsync(admin, "Admin");
               await roleManager.AssignPermissionsToAdmin();
            }
        }

        public static async Task AssignPermissionsToAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var admin = await roleManager.FindByNameAsync("Admin");
            foreach(model item in Enum.GetValues(typeof(model)))
            {
              await roleManager.SeedPermissionForAllModels(item.ToString(), admin);

            }
        }

        public static async Task SeedPermissionForAllModels(this RoleManager<IdentityRole> roleManager, string model, IdentityRole role)
        {
            var allPermission = Permissions.GetPermissionforModel(model);
            var claimsRole = await roleManager.GetClaimsAsync(role);
            foreach (var item in allPermission)
            {
                if (!claimsRole.Any(a => a.Type == Permission && a.Value == item))
                    await roleManager.AddClaimAsync(role, new Claim(Permission, item));
            }
        }


    }
}
