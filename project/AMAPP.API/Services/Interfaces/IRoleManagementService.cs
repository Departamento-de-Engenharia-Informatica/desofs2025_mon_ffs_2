namespace AMAPP.API.Services.Interfaces
{
    public interface IRoleManagementService
    {
        Task<bool> AssignRolesToUserAsync(string userId, List<string> roleNames);
        Task<bool> RemoveRolesFromUserAsync(string userId, List<string> roleNames);
        Task<bool> UserExistsByNameAsync(string userName);
        Task<bool> RoleExistsAsync(string roleName);
    }
}
