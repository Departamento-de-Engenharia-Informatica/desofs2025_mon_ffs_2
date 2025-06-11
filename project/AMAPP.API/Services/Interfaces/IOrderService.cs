using AMAPP.API.DTOs.Order;
using AMAPP.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMAPP.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderFilterDTO filter, string userId);
        Task<OrderDetailDTO> GetOrderByIdAsync(int orderId, string userId);

        // Coproducer-related methods
        Task<IEnumerable<OrderDTO>> GetOrdersByCoproducerAsync(int coproducerId);
        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId);
        Task<int> GetCoproducerIdForUserAsync(string userId);

        // Producer-related methods
        Task<IEnumerable<OrderDTO>> GetOrdersByProducerAsync(int producerId);
        Task<IEnumerable<OrderDTO>> GetOrdersByProducerUserIdAsync(string userId);
        Task<int> GetProducerIdForUserAsync(string userId);

        // Order operations
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO, string userId);
        Task<OrderDTO> UpdateOrderAsync(int orderId, UpdateOrderDTO updateOrderDTO, string userId);
        Task<bool> CancelOrderAsync(int orderId, string userId); 
        Task<bool> CancelOrderItemAsync(int orderItemId, string userId); 

        // Order item operations
        Task<OrderItemDTO> UpdateOrderItemAsync(int orderItemId, UpdateOrderItemDTO updateOrderItemDTO, string userId);
        Task<OrderItemDTO> AddOrderItemAsync(int orderId, CreateOrderItemDTO createOrderItemDTO, string userId);
        Task<bool> RemoveOrderItemAsync(int orderItemId, string userId);

        // Authorization checks
        Task<bool> CanCoproducerModifyOrderAsync(int orderId, string userId);
        Task<bool> CanProducerModifyOrderItemAsync(int orderItemId, string userId);
        Task<bool> CanUserUpdateOrderAsync(int orderId, string userId); 
        Task<bool> CanUserUpdateOrderItemAsync(int orderItemId, string userId); 
        Task<bool> CanUserAccessOrderAsync(int orderId, string userId);
    }
}