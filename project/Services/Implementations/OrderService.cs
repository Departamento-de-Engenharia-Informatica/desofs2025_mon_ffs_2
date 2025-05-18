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

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderFilterDTO filter)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.CoproducerInfo)
                .Include(o => o.Reservation)
                .AsQueryable();

            // Apply filters
            if (filter.StartDate.HasValue)
                query = query.Where(o => o.OrderDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(o => o.OrderDate <= filter.EndDate.Value);

            if (filter.Status.HasValue)
                query = query.Where(o => o.Status == filter.Status.Value);

            if (filter.CoproducerId.HasValue)
                query = query.Where(o => o.CoproducerInfoId == filter.CoproducerId.Value);

            // if (filter.ProductId.HasValue)
            //     query = query.Where(o => o.OrderItems.Any(oi => oi.ProductId == filter.ProductId.Value));

            if (filter.ProducerId.HasValue)
                query = query.Where(o => o.OrderItems.Any(oi => oi.ProducerId == filter.ProducerId.Value));

            // Apply sorting
            query = ApplySorting(query, filter.SortBy, filter.Descending);

            var orders = await query.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDetailDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.CoproducerInfo)
                .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return null;

            return _mapper.Map<OrderDetailDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCoproducerAsync(int coproducerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CoproducerInfoId == coproducerId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Reservation)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByProducerAsync(int producerId)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderItems.Any(oi => oi.ProducerId == producerId))
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.CoproducerInfo)
                .Include(o => o.Reservation)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        /* Example of previous implementation for CreateOrderAsync

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            var coproducer = await _context.CoproducersInfo
               .FirstOrDefaultAsync(c => c.Id == createOrderDTO.CoproducerInfoId);

            if (coproducer == null)
                throw new KeyNotFoundException($"Coproducer with ID {createOrderDTO.CoproducerInfoId} not found");

            // Create new order
            var order = new Order
            {
                CoproducerInfoId = createOrderDTO.CoproducerInfoId,
                OrderDate = DateTime.UtcNow,
                DeliveryRequirements = createOrderDTO.DeliveryRequirements,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            // Add order items
            foreach (var itemDto in createOrderDTO.OrderItems)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == itemDto.ProductId);

                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} not found");

                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = product.ReferencePrice,
                    ProducerId = product.ProducerInfoId
                };

                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(order);
        } */

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            try
            {
                // Find or create CoproducerInfo by UserId
                var coproducerInfo = await _context.CoproducersInfo
                    .Include(c => c.User) // Include User here if you want to avoid repeating later
                    .FirstOrDefaultAsync(c => c.UserId == createOrderDTO.UserId);

                if (coproducerInfo == null)
                {
                    coproducerInfo = new CoproducerInfo
                    {
                        UserId = createOrderDTO.UserId
                    };

                    _context.CoproducersInfo.Add(coproducerInfo);
                    await _context.SaveChangesAsync(); // important to generate the Id
                }

                // Create new order
                var order = new Order
                {
                    CoproducerInfoId = coproducerInfo.Id,
                    OrderDate = DateTime.UtcNow,
                    DeliveryRequirements = createOrderDTO.DeliveryRequirements,
                    Status = OrderStatus.Pending,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var itemDto in createOrderDTO.OrderItems)
                {
                    var product = await _context.Products
                        .Include(p => p.ProducerInfo)
                            .ThenInclude(pi => pi.User)
                        .FirstOrDefaultAsync(p => p.Id == itemDto.ProductId);

                    if (product == null)
                        throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} was not found.");

                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = itemDto.Quantity,
                        Price = product.ReferencePrice,
                        ProducerId = product.ProducerInfoId
                    });
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Reload the full order with related data
                var fullOrder = await _context.Orders
                    .Include(o => o.CoproducerInfo)
                        .ThenInclude(ci => ci.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                            .ThenInclude(p => p.ProducerInfo)
                                .ThenInclude(pi => pi.User)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                return _mapper.Map<OrderDTO>(fullOrder);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving the order to the database: " + ex.InnerException?.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating the order: " + ex.Message, ex);
            }
        }

        public async Task<OrderDTO> UpdateOrderAsync(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.CoproducerInfo)
                .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found");

            // Update order properties
            order.DeliveryRequirements = updateOrderDTO.DeliveryRequirements;
            order.Status = updateOrderDTO.Status;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderItemDTO> UpdateOrderItemAsync(int orderItemId, UpdateOrderItemDTO updateOrderItemDTO)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                throw new KeyNotFoundException($"Order item with ID {orderItemId} not found");

            // Update item quantity
            orderItem.Quantity = updateOrderItemDTO.Quantity;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDTO>(orderItem);
        }

        public async Task<OrderItemDTO> AddOrderItemAsync(int orderId, CreateOrderItemDTO createOrderItemDTO)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found");

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == createOrderItemDTO.ProductId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {createOrderItemDTO.ProductId} not found");

            // Check if the product already exists in the order
            var existingItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == createOrderItemDTO.ProductId);

            if (existingItem != null)
            {
                // Update the quantity if the product already exists in the order
                existingItem.Quantity += createOrderItemDTO.Quantity;
                await _context.SaveChangesAsync();

                return _mapper.Map<OrderItemDTO>(existingItem);
            }

            // Create new order item
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = createOrderItemDTO.ProductId,
                Quantity = createOrderItemDTO.Quantity,
                Price = product.ReferencePrice,
                ProducerId = product.ProducerInfoId
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDTO>(orderItem);
        }

        public async Task<bool> RemoveOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                return false;

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CanCoproducerModifyOrderAsync(int coproducerId, int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            // Check if the order belongs to the coproducer
            return order.CoproducerInfoId == coproducerId;
        }

        public async Task<bool> CanProducerModifyOrderItemAsync(int producerId, int orderItemId)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                return false;

            // Check if the order item contains a product from the producer
            return orderItem.ProducerId == producerId;
        }

        private IQueryable<Order> ApplySorting(IQueryable<Order> query, string sortBy, bool descending)
        {
            switch (sortBy.ToLower())
            {
                case "date":
                case "orderdate":
                    return descending
                        ? query.OrderByDescending(o => o.OrderDate)
                        : query.OrderBy(o => o.OrderDate);
                case "status":
                    return descending
                        ? query.OrderByDescending(o => o.Status)
                        : query.OrderBy(o => o.Status);
                case "coproducer":
                case "coproducerid":
                    return descending
                        ? query.OrderByDescending(o => o.CoproducerInfoId)
                        : query.OrderBy(o => o.CoproducerInfoId);
                default:
                    return descending
                        ? query.OrderByDescending(o => o.OrderDate)
                        : query.OrderBy(o => o.OrderDate);
            }
        }
    }
}
