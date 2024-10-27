using Microsoft.AspNetCore.Identity;

namespace HMSBusinessLogic.Seeds
{
    public static class SeedRoleAdmin
    {
        public static async Task SeedAdminRole(RoleManager<IdentityRole> roleManager)
        {
           IdentityRole result= await roleManager.FindByNameAsync("Admin");
            if (result == null)
            {
                IdentityRole role = new()
                {
                    Name = "Admin"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
