using AMAPP.API.Models;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AMAPP.API.Services.Implementations
{
    public class RoleManagementService : IRoleManagementService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRoleInfoService _userRoleInfoService;
        private readonly ILogger<RoleManagementService> _logger;

        public RoleManagementService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserRoleInfoService userRoleInfoService,
            ILogger<RoleManagementService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRoleInfoService = userRoleInfoService;
            _logger = logger;
        }

        public async Task<bool> AssignRolesToUserAsync(string userName, List<string> roleNames)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    _logger.LogWarning("User {UserId} not found for role assignment", userName);
                    return false;
                }

                var validRoles = new List<string>();
                foreach (var roleName in roleNames)
                {
                    if (await _roleManager.RoleExistsAsync(roleName))
                    {
                        validRoles.Add(roleName);
                    }
                    else
                    {
                        _logger.LogWarning("Role {RoleName} does not exist", roleName);
                    }
                }

                if (!validRoles.Any())
                {
                    _logger.LogWarning("No valid roles to assign to user {UserId}", userName);
                    return false;
                }

                var result = await _userManager.AddToRolesAsync(user, validRoles);
                if (result.Succeeded)
                {
                    // ✅ CRIAR AUTOMATICAMENTE ProducerInfo e CoproducerInfo
                    try
                    {
                        await _userRoleInfoService.CreateRoleInfoAsync(userName, validRoles);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating role info for user {UserId}", userName);
                        // Reverter roles se falhar na criação das info
                        await _userManager.RemoveFromRolesAsync(user, validRoles);
                        return false;
                    }

                    _logger.LogInformation("Successfully assigned roles {Roles} to user {UserId}",
                        string.Join(", ", validRoles), userName);
                    return true;
                }

                _logger.LogWarning("Failed to assign roles to user {UserId}: {Errors}",
                    userName, string.Join(", ", result.Errors.Select(e => e.Description)));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning roles to user {UserId}", userName);
                return false;
            }
        }

        public async Task<bool> RemoveRolesFromUserAsync(string userName, List<string> roleNames)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    _logger.LogWarning("User {UserId} not found for role removal", userName);
                    return false;
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var rolesToRemove = roleNames.Where(r => userRoles.Contains(r)).ToList();

                if (!rolesToRemove.Any())
                {
                    _logger.LogWarning("No roles to remove from user {UserId}", userName);
                    return true; // Consider this success since the desired state is achieved
                }

                var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Successfully removed roles {Roles} from user {UserId}",
                        string.Join(", ", rolesToRemove), userName);
                    return true;
                }

                _logger.LogWarning("Failed to remove roles from user {UserId}: {Errors}",
                    userName, string.Join(", ", result.Errors.Select(e => e.Description)));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing roles from user {UserId}", userName);
                return false;
            }
        }

        public async Task<bool> UserExistsByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;

            var user = await _userManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return false;

            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
