﻿using AMAPP.API.DTOs;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders([FromQuery] OrderFilterDTO filter)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Retrieving orders for user: {UserId}", userId);
                var orders = await _orderService.GetOrdersAsync(filter, userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving orders");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving orders" });
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OrderDetailDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDetailDTO>> GetOrder(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Retrieving order with ID: {OrderId}", id);
                var order = await _orderService.GetOrderByIdAsync(id, userId);

                if (order == null)
                {
                    _logger.LogWarning("Order with ID: {OrderId} not found", id);
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while retrieving order {OrderId}", id);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order with ID: {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the order" });
            }
        }

        [HttpGet("Coproducer/{coproducerId:int}")]
        [Authorize(Roles = "CoProducer,Administrator")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByCoproducer(int coproducerId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Verify if the current user is authorized for this coproducer
                if (!await IsCurrentUserAuthorizedForCoproducer(coproducerId, userId))
                {
                    _logger.LogWarning("User not authorized for coproducer ID: {CoproducerId}", coproducerId);
                    return Forbid();
                }

                _logger.LogInformation("Retrieving orders for coproducer ID: {CoproducerId}", coproducerId);
                var orders = await _orderService.GetOrdersByCoproducerAsync(coproducerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders for coproducer ID: {CoproducerId}", coproducerId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the orders" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "CoProducer")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for order creation");
                return BadRequest(new
                {
                    message = "Invalid order data",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Creating new order for user: {UserId}", userId);
                var order = await _orderService.CreateOrderAsync(createOrderDTO, userId);

                return CreatedAtAction(
                    nameof(GetOrder),
                    new { id = order.Id },
                    order);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid input while creating order");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating order");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while creating the order", detail = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "CoProducer,Producer")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for order update");
                return BadRequest(new
                {
                    message = "Invalid order data",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Updating order with ID: {OrderId} by user: {UserId}", id, userId);
                var updatedOrder = await _orderService.UpdateOrderAsync(id, updateOrderDTO, userId);
                return Ok(updatedOrder);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Order not found while updating. ID: {OrderId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid input while updating order {OrderId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while updating order {OrderId}", id);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating order {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while updating the order" });
            }
        }

        [HttpPatch("{id:int}/cancel")]
        [Authorize(Roles = "CoProducer,Producer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CancelOrder(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Cancelling order with ID: {OrderId} by user: {UserId}", id, userId);
                var result = await _orderService.CancelOrderAsync(id, userId);

                if (!result)
                {
                    _logger.LogWarning("Order with ID: {OrderId} not found", id);
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                return Ok(new { message = "Order cancelled successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while cancelling order {OrderId}", id);
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while cancelling order {OrderId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while cancelling order {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while cancelling the order" });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "CoProducer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Cancelling order with ID: {OrderId} by coproducer: {UserId}", id, userId);
                var result = await _orderService.CancelOrderAsync(id, userId);

                if (!result)
                {
                    _logger.LogWarning("Order with ID: {OrderId} not found", id);
                    return NotFound(new { message = $"Order with ID {id} not found" });
                }

                return Ok(new { message = "Order cancelled successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while cancelling order {OrderId}", id);
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while cancelling order {OrderId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while cancelling order {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while cancelling the order" });
            }
        }

        [HttpPost("{id:int}/Items")]
        [Authorize(Roles = "CoProducer")]
        [ProducesResponseType(typeof(OrderItemDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderItemDTO>> AddOrderItem(int id, [FromBody] CreateOrderItemDTO createOrderItemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for order item creation");
                return BadRequest(new
                {
                    message = "Invalid order item data",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Adding item to order with ID: {OrderId} by user: {UserId}", id, userId);
                var orderItem = await _orderService.AddOrderItemAsync(id, createOrderItemDTO, userId);

                return CreatedAtAction(
                    nameof(GetOrder),
                    new { id = id },
                    orderItem);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found while adding order item");
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid input while adding order item");
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while adding order item");
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while adding order item to order {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while adding the order item" });
            }
        }

        [HttpDelete("Items/{id:int}")]
        [Authorize(Roles = "CoProducer,Producer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveOrderItem(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                // Check if user is a producer - if so, use the new cancel item method
                if (User.IsInRole("Producer"))
                {
                    _logger.LogInformation("Producer cancelling order item with ID: {OrderItemId} by user: {UserId}", id, userId);
                    var result = await _orderService.CancelOrderItemAsync(id, userId);

                    if (!result)
                    {
                        _logger.LogWarning("Order item with ID: {OrderItemId} not found", id);
                        return NotFound(new { message = $"Order item with ID {id} not found" });
                    }

                    return Ok(new { message = "Order item cancelled successfully" });
                }
                else
                {
                    // Coproducer removing items from their own order
                    _logger.LogInformation("Coproducer removing order item with ID: {OrderItemId} by user: {UserId}", id, userId);
                    var result = await _orderService.RemoveOrderItemAsync(id, userId);

                    if (!result)
                    {
                        _logger.LogWarning("Order item with ID: {OrderItemId} not found", id);
                        return NotFound(new { message = $"Order item with ID {id} not found" });
                    }

                    return NoContent();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while removing order item {OrderItemId}", id);
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while removing order item {OrderItemId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while removing order item {OrderItemId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while removing the order item" });
            }
        }

        [HttpGet("Producer/{producerId:int}")]
        [Authorize(Roles = "Producer,Administrator")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByProducer(int producerId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Verify if the current user is authorized for this producer
                if (!await IsCurrentUserAuthorizedForProducer(producerId, userId))
                {
                    _logger.LogWarning("User not authorized for producer ID: {ProducerId}", producerId);
                    return Forbid();
                }

                _logger.LogInformation("Retrieving orders for producer ID: {ProducerId}", producerId);
                var orders = await _orderService.GetOrdersByProducerAsync(producerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders for producer ID: {ProducerId}", producerId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the orders" });
            }
        }

        [HttpPut("Items/{id:int}")]
        [Authorize(Roles = "CoProducer,Producer")]
        [ProducesResponseType(typeof(OrderItemDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderItemDTO>> UpdateOrderItem(int id, [FromBody] UpdateOrderItemDTO updateOrderItemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for order item update");
                return BadRequest(new
                {
                    message = "Invalid order item data",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims");
                    return Unauthorized(new { message = "User not authenticated properly" });
                }

                _logger.LogInformation("Updating order item with ID: {OrderItemId} by user: {UserId}", id, userId);
                var orderItem = await _orderService.UpdateOrderItemAsync(id, updateOrderItemDTO, userId);
                return Ok(orderItem);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Order item not found while updating. ID: {OrderItemId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid input while updating order item {OrderItemId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access while updating order item {OrderItemId}", id);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating order item {OrderItemId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while updating the order item" });
            }
        }

        // --------------------------
        // HELPER METHODS
        // --------------------------

        private async Task<bool> IsCurrentUserAuthorizedForCoproducer(int coproducerId, string userId)
        {
            // Check if user is admin
            if (User.IsInRole("Administrator"))
                return true;

            // Check if the current user's coproducer ID matches
            var userCoproducerId = await _orderService.GetCoproducerIdForUserAsync(userId);
            return userCoproducerId == coproducerId;
        }

        private async Task<bool> IsCurrentUserAuthorizedForProducer(int producerId, string userId)
        {
            // Check if user is admin
            if (User.IsInRole("Administrator"))
                return true;

            // Check if the current user's producer ID matches
            var userProducerId = await _orderService.GetProducerIdForUserAsync(userId);
            return userProducerId == producerId;
        }
    }
}