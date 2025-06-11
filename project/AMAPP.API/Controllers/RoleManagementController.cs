using AMAPP.API.DTOs.RoleManagement;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")] // Apenas administradores
    public class RoleManagementController : ControllerBase
    {
        private readonly IRoleManagementService _roleManagementService;
        private readonly ILogger<RoleManagementController> _logger;

        public RoleManagementController(
            IRoleManagementService roleManagementService,
            ILogger<RoleManagementController> logger)
        {
            _roleManagementService = roleManagementService;
            _logger = logger;
        }

        /// <summary>
        /// Adicionar roles a um usuário
        /// </summary>
        [HttpPost("assign-roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRoleDto dto)
        {
            try
            {
                // FluentValidation executa automaticamente aqui
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for assign roles request to user {UserName}", dto.UserName);
                    return BadRequest(ModelState);
                }

                // Verificar se o usuário existe pelo username (email)
                if (!await _roleManagementService.UserExistsByNameAsync(dto.UserName))
                {
                    _logger.LogWarning("User with username {UserName} not found", dto.UserName);
                    return NotFound(new
                    {
                        message = $"User with username '{dto.UserName}' not found",
                        userName = dto.UserName
                    });
                }

                // Tentar adicionar os roles
                var success = await _roleManagementService.AssignRolesToUserAsync(dto.UserName, dto.RoleNames);
                if (!success)
                {
                    _logger.LogWarning("Failed to assign roles {Roles} to user {UserName}",
                        string.Join(", ", dto.RoleNames), dto.UserName);
                    return BadRequest(new
                    {
                        message = "Failed to assign roles to user",
                        userName = dto.UserName,
                        requestedRoles = dto.RoleNames
                    });
                }

                // Log de sucesso para auditoria
                _logger.LogInformation("Admin successfully assigned roles {Roles} to user {UserName}",
                    string.Join(", ", dto.RoleNames), dto.UserName);

                // Resposta de sucesso com detalhes
                return Ok(new
                {
                    message = "Roles assigned successfully",
                    userName = dto.UserName,
                    assignedRoles = dto.RoleNames,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning roles {Roles} to user {UserName}",
                    string.Join(", ", dto.RoleNames ?? new List<string>()), dto.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = "An error occurred while assigning roles",
                        userName = dto.UserName
                    });
            }
        }

        /// <summary>
        /// Remover roles de um usuário
        /// </summary>
        [HttpPost("remove-roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRolesFromUser([FromBody] RemoveRoleDto dto)
        {
            try
            {
                // FluentValidation executa automaticamente aqui
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for remove roles request from user {UserName}", dto.UserName);
                    return BadRequest(ModelState);
                }

                // Verificar se o usuário existe pelo username (email)
                if (!await _roleManagementService.UserExistsByNameAsync(dto.UserName))
                {
                    _logger.LogWarning("User with username {UserName} not found", dto.UserName);
                    return NotFound(new
                    {
                        message = $"User with username '{dto.UserName}' not found",
                        userName = dto.UserName
                    });
                }

                // Tentar remover os roles
                var success = await _roleManagementService.RemoveRolesFromUserAsync(dto.UserName, dto.RoleNames);
                if (!success)
                {
                    _logger.LogWarning("Failed to remove roles {Roles} from user {UserName}",
                        string.Join(", ", dto.RoleNames), dto.UserName);
                    return BadRequest(new
                    {
                        message = "Failed to remove roles from user",
                        userName = dto.UserName,
                        requestedRoles = dto.RoleNames
                    });
                }

                // Log de sucesso para auditoria
                _logger.LogInformation("Admin successfully removed roles {Roles} from user {UserName}",
                    string.Join(", ", dto.RoleNames), dto.UserName);

                // Resposta de sucesso com detalhes
                return Ok(new
                {
                    message = "Roles removed successfully",
                    userName = dto.UserName,
                    removedRoles = dto.RoleNames,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing roles {Roles} from user {UserName}",
                    string.Join(", ", dto.RoleNames ?? new List<string>()), dto.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = "An error occurred while removing roles",
                        userName = dto.UserName
                    });
            }
        }
    }
}
