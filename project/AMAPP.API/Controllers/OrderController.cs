using AMAPP.API.DTOs;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // --------------------------
        // GENERAL (any authenticated user)
        // --------------------------

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders([FromQuery] OrderFilterDTO filter)
        {
            var orders = await _orderService.GetOrdersAsync(filter);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound();

            return order;
        }

        // --------------------------
        // COPRODUCER
        // --------------------------

        [HttpGet("Coproducer/{coproducerId}")]
        [Authorize(Roles = "CoProducer,Administrator")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByCoproducer(int coproducerId)
        {
            if (!await IsCurrentUserAuthorizedForCoproducer(coproducerId))
                return Forbid();

            var orders = await _orderService.GetOrdersByCoproducerAsync(coproducerId);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize(Roles = "CoProducer")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated");

            try
            {
                var order = await _orderService.CreateOrderAsync(createOrderDTO);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error while saving order.", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "CoProducer")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            if (!await IsCurrentUserAuthorizedForCoproducer(order.CoproducerInfoId))
                return Forbid();

            try
            {
                var updatedOrder = await _orderService.UpdateOrderAsync(id, updateOrderDTO);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Items")]
        [Authorize(Roles = "CoProducer")]
        public async Task<ActionResult<OrderItemDTO>> AddOrderItem(int id, CreateOrderItemDTO createOrderItemDTO)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            if (!await IsCurrentUserAuthorizedForCoproducer(order.CoproducerInfoId))
                return Forbid();

            try
            {
                var orderItem = await _orderService.AddOrderItemAsync(id, createOrderItemDTO);
                return CreatedAtAction(nameof(GetOrder), new { id }, orderItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Items/{id}")]
        [Authorize(Roles = "CoProducer")]
        public async Task<ActionResult> RemoveOrderItem(int id)
        {
            // Permission validation can be implemented here if needed

            var result = await _orderService.RemoveOrderItemAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // --------------------------
        // PRODUCER
        // --------------------------

        [HttpGet("Producer/{producerId}")]
        [Authorize(Roles = "Producer,Administrator")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByProducer(int producerId)
        {
            if (!await IsCurrentUserAuthorizedForProducer(producerId))
                return Forbid();

            var orders = await _orderService.GetOrdersByProducerAsync(producerId);
            return Ok(orders);
        }

        // --------------------------
        // COPRODUCER OR PRODUCER (both can access)
        // --------------------------

        [HttpPut("Items/{id}")]
        [Authorize(Roles = "CoProducer,Producer")]
        public async Task<ActionResult<OrderItemDTO>> UpdateOrderItem(int id, UpdateOrderItemDTO updateOrderItemDTO)
        {
            try
            {
                var orderItem = await _orderService.UpdateOrderItemAsync(id, updateOrderItemDTO);
                return Ok(orderItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // --------------------------
        // HELPER METHODS
        // --------------------------

        private async Task<bool> IsCurrentUserAuthorizedForCoproducer(int coproducerId)
        {
            // TODO: actual verification logic
            return true;
        }

        private async Task<bool> IsCurrentUserAuthorizedForProducer(int producerId)
        {
            // TODO: actual verification logic
            return true;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return -1;

            return int.TryParse(userIdClaim.Value, out int userId) ? userId : -1;
        }
    }
}