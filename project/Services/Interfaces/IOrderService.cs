using AMAPP.API.DTOs.Order;

namespace AMAPP.API.Services.Interfaces
{
    public interface IOrderService
    {
        // REQ-10: Display all active and completed orders
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();

        // REQ-11 & REQ-13: Provide detailed information for each order
        Task<OrderDetailDTO> GetOrderByIdAsync(int id);

        // REQ-12: Allow filtering and sorting orders
        Task<IEnumerable<OrderDTO>> GetFilteredOrdersAsync(OrderFilterDTO filterDto);

        // REQ-21: Co-producer can make new order
        Task<OrderDetailDTO> CreateOrderAsync(OrderCreateDTO orderDto);

        // REQ-22: Co-producer can see the details of their orders
        Task<IEnumerable<OrderDTO>> GetCoproducerOrdersAsync(int coproducerId);

        // REQ-23: Co-producer can update their orders
        Task<OrderDetailDTO> UpdateCoproducerOrderAsync(int id, OrderUpdateDTO orderDto);

        // REQ-24: Producer can see the list of orders for their products
        Task<IEnumerable<OrderDTO>> GetProducerOrdersAsync(int producerId);

        // REQ-25: Producer can update orders that contain their products
        Task<OrderDetailDTO> UpdateProduceOrderStatusAsync(int id, OrderStatusUpdateDTO statusDto);
    }
}
