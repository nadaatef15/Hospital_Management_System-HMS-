using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;

namespace HMSDataAccess.Reposatory.Identity
{
    public interface IRoleReposatory
    {
        Task AddRole(string roleName);
        Task DeleteRole(IdentityRole roleId);
        Task UpdateRole(IdentityRole role);
        Task<IQueryable<IdentityRole>> GetRoles();

    }
    public class RoleReposatory : IRoleReposatory
    {
        RoleManager<IdentityRole> roleManager;
        public RoleReposatory(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        public async Task AddRole(string roleName)
        {
                IdentityRole role = new()
                {
                    Name = roleName,
                };
                await roleManager.CreateAsync(role);
        }

        public async Task DeleteRole(IdentityRole role)
        {
            await roleManager.DeleteAsync(role);
        }

        public async Task UpdateRole(IdentityRole role)=>
           await roleManager.UpdateAsync(role);
        

        public async Task<IQueryable<IdentityRole>> GetRoles()
        {
            return roleManager.Roles.AsQueryable();
        }


    }
}
