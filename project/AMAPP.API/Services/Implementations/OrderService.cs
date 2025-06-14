﻿using AMAPP.API.Data;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Models;
using AMAPP.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AMAPP.API.Constants;

namespace AMAPP.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public OrderService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderFilterDTO filter, string? userId = null)
        {
            // If userId is provided, check if the user is an admin
            bool isAdmin = false;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");
            }

            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.ProducerInfo)
                            .ThenInclude(pi => pi.User)
                .Include(o => o.CoproducerInfo)
                    .ThenInclude(ci => ci.User)
                .Include(o => o.Reservation)
                .AsQueryable();

            // If not admin and userId is provided, only return orders they're authorized to see
            if (!isAdmin && !string.IsNullOrEmpty(userId))
            {
                // Get producer and coproducer IDs for this user
                var producerId = await GetProducerIdForUserAsync(userId);
                var coproducerId = await GetCoproducerIdForUserAsync(userId);

                // Filter to only show orders related to this user
                query = query.Where(o =>
                    (coproducerId > 0 && o.CoproducerInfoId == coproducerId) ||
                    (producerId > 0 && o.OrderItems.Any(oi => oi.ProducerId == producerId))
                );
            }

            // Apply filters
            if (filter.StartDate.HasValue)
                query = query.Where(o => o.OrderDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(o => o.OrderDate <= filter.EndDate.Value);

            if (filter.Status.HasValue)
                query = query.Where(o => o.Status == filter.Status.Value);

            if (filter.CoproducerId.HasValue)
                query = query.Where(o => o.CoproducerInfoId == filter.CoproducerId.Value);

            if (filter.ProducerId.HasValue)
                query = query.Where(o => o.OrderItems.Any(oi => oi.ProducerId == filter.ProducerId.Value));

            // Apply sorting
            query = ApplySorting(query, filter.SortBy, filter.Descending);

            var orders = await query.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDetailDTO> GetOrderByIdAsync(int orderId, string? userId = null)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.ProducerInfo)
                            .ThenInclude(pi => pi.User)
                .Include(o => o.CoproducerInfo)
                    .ThenInclude(ci => ci.User)
                .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return null;

            // If userId is provided, check authorization
            if (!string.IsNullOrEmpty(userId))
            {
                // Check if user is admin
                var user = await _userManager.FindByIdAsync(userId);
                var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

                // If not admin, check if user is authorized to see this order
                if (!isAdmin)
                {
                    bool isAuthorized = await CanUserAccessOrderAsync(orderId, userId);
                    if (!isAuthorized)
                    {
                        throw new UnauthorizedAccessException("You don't have permission to view this order.");
                    }
                }
            }

            return _mapper.Map<OrderDetailDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCoproducerAsync(int coproducerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CoproducerInfoId == coproducerId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.ProducerInfo)
                            .ThenInclude(pi => pi.User)
                .Include(o => o.CoproducerInfo)
                    .ThenInclude(ci => ci.User)
                .Include(o => o.Reservation)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId)
        {
            // Find coproducer info for this user
            var coproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (coproducerInfo == null)
                return new List<OrderDTO>();

            return await GetOrdersByCoproducerAsync(coproducerInfo.Id);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByProducerAsync(int producerId)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderItems.Any(oi => oi.ProducerId == producerId))
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.ProducerInfo)
                            .ThenInclude(pi => pi.User)
                .Include(o => o.CoproducerInfo)
                    .ThenInclude(ci => ci.User)
                .Include(o => o.Reservation)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByProducerUserIdAsync(string userId)
        {
            // Find producer info for this user
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (producerInfo == null)
                return new List<OrderDTO>();

            return await GetOrdersByProducerAsync(producerInfo.Id);
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO, string userId)
        {
            try
            {
                // Validate the user exists
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    throw new KeyNotFoundException("User not found.");

                // Find or create CoproducerInfo by UserId
                var coproducerInfo = await _context.CoproducersInfo
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (coproducerInfo == null)
                {
                    coproducerInfo = new CoproducerInfo
                    {
                        UserId = userId
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

        public async Task<OrderDTO> UpdateOrderAsync(int orderId, UpdateOrderDTO updateOrderDTO, string userId)
        {
            // Get the order first to check if it exists
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.ProducerInfo)
                .Include(o => o.CoproducerInfo)
                .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found");

            // Check authorization - both coproducer (order owner) and producers (with items in order) can update
            bool canUpdate = await CanUserUpdateOrderAsync(orderId, userId);
            if (!canUpdate)
            {
                throw new UnauthorizedAccessException("You don't have permission to update this order.");
            }

            // Update order properties
            order.DeliveryRequirements = updateOrderDTO.DeliveryRequirements;
            order.Status = updateOrderDTO.Status;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(order);
        }

        // New method: Soft delete - changes order status to Cancelled (Only coproducers)
        public async Task<bool> CancelOrderAsync(int orderId, string userId)
        {
            var order = await _context.Orders
                .Include(o => o.CoproducerInfo)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            // Check if user is admin first
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

            if (!isAdmin)
            {
                // Only the coproducer who owns the order can cancel the entire order
                bool isCoproducerOwner = await CanCoproducerModifyOrderAsync(orderId, userId);
                if (!isCoproducerOwner)
                {
                    throw new UnauthorizedAccessException("Only the coproducer who placed the order can cancel it.");
                }
            }

            // Check if order can be cancelled (not already completed or delivered)
            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Delivered)
            {
                throw new InvalidOperationException("Cannot cancel an order that is already completed or delivered.");
            }

            // Soft delete: change status to Cancelled
            order.Status = OrderStatus.Cancelled;
            await _context.SaveChangesAsync();

            return true;
        }

        // New method: Cancel specific order item (Producers can cancel their own items)
        public async Task<bool> CancelOrderItemAsync(int orderItemId, string userId)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Product)
                    .ThenInclude(p => p.ProducerInfo)
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                return false;

            // Check if user is admin first
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

            if (!isAdmin)
            {
                // Check if order can be modified (not already completed or delivered)
                if (orderItem.Order.Status == OrderStatus.Completed || orderItem.Order.Status == OrderStatus.Delivered)
                {
                    throw new InvalidOperationException("Cannot cancel items from an order that is already completed or delivered.");
                }

                // Only the producer who owns this specific item can cancel it
                bool isProducerOwner = await CanProducerModifyOrderItemAsync(orderItemId, userId);
                if (!isProducerOwner)
                {
                    throw new UnauthorizedAccessException("Only the producer who owns this item can cancel it.");
                }
            }

            // Remove the order item (hard delete for individual items)
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            // If this was the last item in the order, cancel the entire order
            var remainingItems = await _context.OrderItems
                .CountAsync(oi => oi.OrderId == orderItem.OrderId);

            if (remainingItems == 0)
            {
                var order = await _context.Orders.FindAsync(orderItem.OrderId);
                if (order != null)
                {
                    order.Status = OrderStatus.Cancelled;
                    await _context.SaveChangesAsync();
                }
            }

            return true;
        }

        public async Task<OrderItemDTO> UpdateOrderItemAsync(int orderItemId, UpdateOrderItemDTO updateOrderItemDTO, string userId)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Product)
                    .ThenInclude(p => p.ProducerInfo)
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                throw new KeyNotFoundException($"Order item with ID {orderItemId} not found");

            // Check authorization - both coproducer (order owner) and producer (item owner) can update
            bool canUpdate = await CanUserUpdateOrderItemAsync(orderItemId, userId);
            if (!canUpdate)
            {
                throw new UnauthorizedAccessException("You don't have permission to update this order item.");
            }

            // Update item quantity
            orderItem.Quantity = updateOrderItemDTO.Quantity;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDTO>(orderItem);
        }

        public async Task<OrderItemDTO> AddOrderItemAsync(int orderId, CreateOrderItemDTO createOrderItemDTO, string userId)
        {
            var order = await _context.Orders
                .Include(o => o.CoproducerInfo)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found");

            // Check if user can modify this order (only coproducer who owns the order or admin)
            bool canModify = await CanCoproducerModifyOrderAsync(orderId, userId);
            if (!canModify)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");
                if (!isAdmin)
                {
                    throw new UnauthorizedAccessException("You don't have permission to modify this order.");
                }
            }

            var product = await _context.Products
                .Include(p => p.ProducerInfo)
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

        public async Task<bool> RemoveOrderItemAsync(int orderItemId, string userId)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.CoproducerInfo)
                .Include(oi => oi.Product)
                    .ThenInclude(p => p.ProducerInfo)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                return false;

            // Check if user can modify this order item (only coproducer who owns the order or admin)
            bool canModify = await CanCoproducerModifyOrderAsync(orderItem.OrderId, userId);
            if (!canModify)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");
                if (!isAdmin)
                {
                    throw new UnauthorizedAccessException("You don't have permission to remove this order item.");
                }
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return true;
        }

        // Authorization Methods

        public async Task<bool> CanCoproducerModifyOrderAsync(int orderId, string userId)
        {
            // Find coproducer info for this user
            var coproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (coproducerInfo == null)
                return false;

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            // Check if the order belongs to the coproducer
            return order.CoproducerInfoId == coproducerInfo.Id;
        }

        public async Task<bool> CanProducerModifyOrderItemAsync(int orderItemId, string userId)
        {
            // Find producer info for this user
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (producerInfo == null)
                return false;

            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                return false;

            // Check if the order item contains a product from the producer
            return orderItem.ProducerId == producerInfo.Id;
        }

        // New authorization method: Check if user can update order (coproducer OR producer with items)
        public async Task<bool> CanUserUpdateOrderAsync(int orderId, string userId)
        {
            // Check if user is admin first
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");
            if (isAdmin)
                return true;

            // Check if user is the coproducer who owns this order
            bool isCoproducerOwner = await CanCoproducerModifyOrderAsync(orderId, userId);
            if (isCoproducerOwner)
                return true;

            // Check if user is a producer with items in this order
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (producerInfo == null)
                return false;

            // Check if any item in the order belongs to this producer
            var hasItems = await _context.OrderItems
                .AnyAsync(oi => oi.OrderId == orderId && oi.ProducerId == producerInfo.Id);

            return hasItems;
        }

        // New authorization method: Check if user can update order item (coproducer OR producer who owns the item)
        public async Task<bool> CanUserUpdateOrderItemAsync(int orderItemId, string userId)
        {
            // Check if user is admin first
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");
            if (isAdmin)
                return true;

            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
                return false;

            // Check if user is the coproducer who owns the order
            bool isCoproducerOwner = await CanCoproducerModifyOrderAsync(orderItem.OrderId, userId);
            if (isCoproducerOwner)
                return true;

            // Check if user is the producer who owns this specific item
            return await CanProducerModifyOrderItemAsync(orderItemId, userId);
        }

        // Existing method: Check if a user can access an order (as either producer or coproducer)
        public async Task<bool> CanUserAccessOrderAsync(int orderId, string userId)
        {
            // Check if user is a coproducer who owns this order
            bool isCoproducerOwner = await CanCoproducerModifyOrderAsync(orderId, userId);
            if (isCoproducerOwner)
                return true;

            // Check if user is a producer with items in this order
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (producerInfo == null)
                return false;

            // Check if any item in the order belongs to this producer
            var hasItems = await _context.OrderItems
                .AnyAsync(oi => oi.OrderId == orderId && oi.ProducerId == producerInfo.Id);

            return hasItems;
        }

        public async Task<int> GetCoproducerIdForUserAsync(string userId)
        {
            var coproducerInfo = await _context.CoproducersInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return coproducerInfo?.Id ?? -1;
        }

        public async Task<int> GetProducerIdForUserAsync(string userId)
        {
            var producerInfo = await _context.ProducersInfo
                .FirstOrDefaultAsync(p => p.UserId == userId);

            return producerInfo?.Id ?? -1;
        }

        private IQueryable<Order> ApplySorting(IQueryable<Order> query, string sortBy, bool descending)
        {
            switch (sortBy?.ToLower())
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