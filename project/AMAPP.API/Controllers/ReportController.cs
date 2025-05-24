using System;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace AMAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(
            IReportService reportService,
            ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }
        
        [HttpGet("download")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Download()
        {
            try
            {
                var pdfBytes = _reportService.GenerateReport();
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var fileName = $"Reservations_Deliveries_Report_{timestamp}.pdf";

                return File(
                    fileContents:     pdfBytes,
                    contentType:      "application/pdf",
                    fileDownloadName: fileName
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating Reservations & Deliveries Report");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while generating the Reservations & Deliveries Report." }
                );
            }
        }
    }
}