using AMAPP.API.DTOs.Order;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication for all actions by default
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // ADMIN and AMAP can view all orders
        [HttpGet]
        [Authorize(Roles = "ADMIN,AMAP")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // ADMIN and AMAP can view any order by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,AMAP")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // ADMIN and AMAP can filter orders
        [HttpPost("filter")]
        [Authorize(Roles = "ADMIN,AMAP")]
        public async Task<IActionResult> GetFilteredOrders([FromBody] OrderFilterDTO filterDto)
        {
            var orders = await _orderService.GetFilteredOrdersAsync(filterDto);
            return Ok(orders);
        }

        // COPR can create orders
        [HttpPost]
        [Authorize(Roles = "COPR")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // COPR can view their own orders
        [HttpGet("coproducer/{coproducerId}")]
        [Authorize(Roles = "COPR")]
        public async Task<IActionResult> GetCoproducerOrders(int coproducerId)
        {
            var orders = await _orderService.GetCoproducerOrdersAsync(coproducerId);
            return Ok(orders);
        }

        // COPR can update their own orders
        [HttpPut("coproducer/{id}")]
        [Authorize(Roles = "COPR")]
        public async Task<IActionResult> UpdateCoproducerOrder(int id, [FromBody] OrderUpdateDTO orderDto)
        {
            var order = await _orderService.UpdateCoproducerOrderAsync(id, orderDto);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // PROD can view their own orders
        [HttpGet("producer/{producerId}")]
        [Authorize(Roles = "PROD")]
        public async Task<IActionResult> GetProducerOrders(int producerId)
        {
            var orders = await _orderService.GetProducerOrdersAsync(producerId);
            return Ok(orders);
        }

        // PROD can update order status for their products
        [HttpPut("producer/{id}/status")]
        [Authorize(Roles = "PROD")]
        public async Task<IActionResult> UpdateProduceOrderStatus(int id, [FromBody] OrderStatusUpdateDTO statusDto)
        {
            var order = await _orderService.UpdateProduceOrderStatusAsync(id, statusDto);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}
