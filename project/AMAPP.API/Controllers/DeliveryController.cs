using AMAPP.API.DTOs.Delivery;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(IDeliveryService deliveryService, ILogger<DeliveryController> logger)
        {
            _deliveryService = deliveryService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Retrieving all deliveries");
                var deliveries = await _deliveryService.GetAllAsync();
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all deliveries");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving deliveries" });
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrator,CoProducer,Producer")]
        [ProducesResponseType(typeof(DeliveryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeliveryDto>> GetById(int id)
        {
            try
            {
                // Get the current user ID from claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Retrieving delivery with ID: {DeliveryId}", id);
                var delivery = await _deliveryService.GetByIdAsync(id, userId);

                if (delivery == null)
                {
                    _logger.LogWarning("Delivery with ID: {DeliveryId} not found", id);
                    return NotFound(new { message = $"Delivery with ID {id} not found" });
                }

                return Ok(delivery);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while retrieving delivery {DeliveryId}", id);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving delivery with ID: {DeliveryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the delivery" });
            }
        }

        [HttpGet("Producer/{producerId:int}")]
        [Authorize(Roles = "Producer,Administrator")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetDeliveriesByProducer(int producerId)
        {
            try
            {
                // Get the current user ID from claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                // Verify if the current user is authorized for this producer
                if (!await IsCurrentUserAuthorizedForProducer(producerId, userId))
                {
                    _logger.LogWarning("User not authorized for producer ID: {ProducerId}", producerId);
                    return Forbid();
                }

                _logger.LogInformation("Retrieving deliveries for producer ID: {ProducerId}", producerId);
                var deliveries = await _deliveryService.GetDeliveriesByProducerAsync(producerId);
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving deliveries for producer ID: {ProducerId}", producerId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the deliveries" });
            }
        }

        [HttpGet("Coproducer/{coproducerId:int}")]
        [Authorize(Roles = "CoProducer,Administrator")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetDeliveriesByCoProducer(int coproducerId)
        {
            try
            {
                // Get the current user ID from claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                // Verify if the current user is authorized for this coproducer
                if (!await IsCurrentUserAuthorizedForCoproducer(coproducerId, userId))
                {
                    _logger.LogWarning("User not authorized for coproducer ID: {CoproducerId}", coproducerId);
                    return Forbid();
                }

                _logger.LogInformation("Retrieving deliveries for coproducer ID: {CoproducerId}", coproducerId);
                var deliveries = await _deliveryService.GetDeliveriesByCoProducerAsync(coproducerId);
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving deliveries for coproducer ID: {CoproducerId}", coproducerId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the deliveries" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(DeliveryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeliveryDto>> Create([FromBody] CreateDeliveryDto createDeliveryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for delivery creation");
                return BadRequest(new
                {
                    message = "Invalid delivery data",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                _logger.LogInformation("Creating new delivery for order: {OrderId}", createDeliveryDto.OrderId);
                var delivery = await _deliveryService.CreateAsync(createDeliveryDto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = delivery.Id },
                    delivery);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found while creating delivery");
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid input while creating delivery");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating delivery");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while creating the delivery" });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(DeliveryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeliveryDto>> Update(int id, [FromBody] UpdateDeliveryDto updateDeliveryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for delivery update");
                return BadRequest(new
                {
                    message = "Invalid delivery data",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                _logger.LogInformation("Updating delivery with ID: {DeliveryId}", id);
                var delivery = await _deliveryService.UpdateAsync(id, updateDeliveryDto);

                if (delivery == null)
                {
                    _logger.LogWarning("Delivery with ID: {DeliveryId} not found for update", id);
                    return NotFound(new { message = $"Delivery with ID {id} not found" });
                }

                return Ok(delivery);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Delivery not found while updating. ID: {DeliveryId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid input while updating delivery {DeliveryId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating delivery {DeliveryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while updating the delivery" });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting delivery with ID: {DeliveryId}", id);
                var result = await _deliveryService.DeleteAsync(id);

                if (!result)
                {
                    _logger.LogWarning("Delivery with ID: {DeliveryId} not found for deletion", id);
                    return NotFound(new { message = $"Delivery with ID {id} not found" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while deleting delivery {DeliveryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while deleting the delivery" });
            }
        }

        // --------------------------
        // HELPER METHODS
        // --------------------------

        private async Task<bool> IsCurrentUserAuthorizedForCoproducer(int coproducerId, string userId)
        {
            // TODO: implementar lógica de verificação real
            // Por agora é um placeholder implementando o mesmo comportamento do código original
            return await _deliveryService.IsUserAuthorizedForCoproducerAsync(userId, coproducerId);
        }

        private async Task<bool> IsCurrentUserAuthorizedForProducer(int producerId, string userId)
        {
            // TODO: implementar lógica de verificação real
            // Por agora é um placeholder implementando o mesmo comportamento do código original
            return await _deliveryService.IsUserAuthorizedForProducerAsync(userId, producerId);
        }
    }
}