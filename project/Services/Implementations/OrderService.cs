using AMAPP.API.Data;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Models;
using AMAPP.API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static AMAPP.API.Constants;

namespace AMAPP.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            // "Active" orders: Pending, Confirmed, Processing, ReadyForDelivery, Delivered
            var activeStatuses = new[]
            {
                OrderStatus.Pending,
                OrderStatus.Confirmed,
                OrderStatus.Processing,
                OrderStatus.ReadyForDelivery,
                OrderStatus.Delivered
            };

            var orders = await _context.Orders
                .Where(o => activeStatuses.Contains(o.Status) || o.Status == OrderStatus.Completed)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDetailDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order == null ? null : _mapper.Map<OrderDetailDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetFilteredOrdersAsync(OrderFilterDTO filterDto)
        {
            var query = _context.Orders.AsQueryable();

            if (filterDto.Status.HasValue)
                query = query.Where(o => o.Status == filterDto.Status.Value);

            if (filterDto.StartDate.HasValue)
                query = query.Where(o => o.OrderDate >= filterDto.StartDate.Value);

            if (filterDto.EndDate.HasValue)
                query = query.Where(o => o.OrderDate <= filterDto.EndDate.Value);

            if (filterDto.CoproducerId.HasValue)
                query = query.Where(o => o.CoproducerInfoId == filterDto.CoproducerId.Value);

            if (filterDto.ProducerId.HasValue)
                query = query.Where(o => o.OrderItems.Any(oi => oi.ProducerId == filterDto.ProducerId.Value));

            if (filterDto.ProductId.HasValue)
                query = query.Where(o => o.OrderItems.Any(oi => oi.ProductId == filterDto.ProductId.Value));

            var orders = await query.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDetailDTO> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDetailDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetCoproducerOrdersAsync(int coproducerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CoproducerInfoId == coproducerId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDetailDTO> UpdateCoproducerOrderAsync(int id, OrderUpdateDTO orderDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            _mapper.Map(orderDto, order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDetailDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetProducerOrdersAsync(int producerId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderItems.Any(oi => oi.ProducerId == producerId))
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDetailDTO> UpdateProduceOrderStatusAsync(int id, OrderStatusUpdateDTO statusDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            order.Status = statusDto.Status;
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDetailDTO>(order);
        }
    }
}
