﻿using HMSContracts.Model.Identity;
using HMSDataAccess.Reposatory.Identity;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSBusinessLogic.Manager.IdentityManager
{
    public interface IRoleManager
    {
        Task createRole(RoleNameModel roleNameDto);
        Task Delete(string roleId);
        Task Update(string roleId, RoleNameModel role);
        Task<IQueryable<string>> GetAll();
    }

    public class RoleManager : IRoleManager
    {
        IRoleReposatory _roleReposatory;
        RoleManager<IdentityRole> _roleManagerIdentity;
        public RoleManager(IRoleReposatory roleReposatory, RoleManager<IdentityRole> roleManagerIdentity)
        {
            _roleReposatory = roleReposatory;
            _roleManagerIdentity = roleManagerIdentity;
        }
        public async Task createRole(RoleNameModel roleNameModel)
        {
            var checkRole = await _roleManagerIdentity.FindByNameAsync(roleNameModel.Name);
            if (checkRole is null)
                await _roleReposatory.AddRole(roleNameModel.Name);
            else
                throw new ConflictException(RoleIsExist);
        }

        public async Task Delete(string roleId)
        {
            var role = await _roleManagerIdentity.FindByIdAsync(roleId);
            if (role is not null)
                await _roleReposatory.DeleteRole(role);
            else
                throw new NotFoundException(RoleDoesnotExist);

        }

        public async Task Update(string roleId, RoleNameModel role)
        {

            var isRole = await _roleManagerIdentity.FindByIdAsync(roleId);
            if (isRole is not null)
            {
                isRole.Name = role.Name;
                await _roleReposatory.UpdateRole(isRole);

            }
            else
                throw new NotFoundException(RoleDoesnotExist);
        }

        public async Task<IQueryable<string>> GetAll() =>
             (await _roleReposatory.GetRoles()).Select(a => a.Name);

    }
}
