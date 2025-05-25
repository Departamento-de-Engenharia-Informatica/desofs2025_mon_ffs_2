using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMAPP.API.DTOs.Reservation;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AMAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(
            IReservationService service,
            ILogger<ReservationController> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReservationDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ReservationDto>>> GetAll()
        {
            try
            {
                var reservations = await _service.GetAllAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all reservations");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving all reservations." }
                );
            }
        }
    }
}
