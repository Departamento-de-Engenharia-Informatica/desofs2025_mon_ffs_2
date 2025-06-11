using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.RoleManagement
{
    public class RemoveRoleDto
    {
        public string UserName { get; set; }
        public List<string> RoleNames { get; set; } = new();
    }
}
