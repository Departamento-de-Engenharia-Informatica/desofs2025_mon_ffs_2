using System;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity; 
using AMAPP.API.DTOs;
using AMAPP.API.Models;

namespace AMAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;      // ← add this

        public ReportController(
            IReportService reportService,
            ILogger<ReportController> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager) 
        {
            _reportService = reportService;
            _logger        = logger;
            _userManager   = userManager;
            _roleManager   = roleManager; 
        }

        [HttpGet("download/{username}")]
        [Authorize(Policy = "CanViewReports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Download([FromRoute] DownloadRequestDto request)
        {
            _logger.LogInformation("Validating download request for selected user");
            var (isError, errorResult, userId) = await AuthorizeDownloadAsync(request.Username);
            if (isError)
                return errorResult;

            try
            {
                _logger.LogInformation("Report generation started.");
                var pdfBytes = _reportService.GenerateReportByUserId(userId);
                var fileName = $"report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                _logger.LogInformation("Report generation completed.");
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No orders"))
            {
                _logger.LogError(ex, "Insufficient data to generate the report.");
                return NotFound(new { message = "No data available for this user." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Report generation failed.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred." });
            }
        }


        private async Task<(bool IsError, IActionResult? Error, string? UserId)> AuthorizeDownloadAsync(string username)
        {
            _logger.LogInformation("Checking user details for download authorization");
            // CoProducer can only download their own report
            if (User.IsInRole("CoProducer"))
            {
                var nameClaim = User.FindFirst(c => c.Type == "name")?.Value;

                if (!string.Equals(nameClaim, username, StringComparison.OrdinalIgnoreCase))
                {
                    return (true,
                        StatusCode(403, new { message = "You may only download your own report." }),
                        null
                    );
                }

                var caller = await _userManager.FindByNameAsync(username);
                if (caller == null)
                    return (true, NotFound(new { message = "The requested report is not available." }), null);
                
                _logger.LogInformation("CoProducer is authorized to download their own report");
                return (false, null, caller.Id);
            }
            
            if (User.IsInRole("Administrator"))
            {
                _logger.LogInformation("Checking selected user details for download authorization");
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                    return (true, NotFound(new { message = "You may only download the report from a valid CoProducer." }), null);

                if (!await _userManager.IsInRoleAsync(user, "COPR"))
                    return (true, Forbid(), null);
                
                _logger.LogInformation("Administrator is authorized authorized to download selected CoProducer report");
                return (false, null, user.Id);
            }


            return (true, Forbid(), null);
        }


    }
}
