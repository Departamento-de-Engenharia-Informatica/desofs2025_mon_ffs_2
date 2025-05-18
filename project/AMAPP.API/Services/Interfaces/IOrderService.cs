using AMAPP.API.DTOs.Order;

namespace AMAPP.API.Services.Interfaces
{
    public interface IOrderService
    {
        // Recuperar pedidos com filtros e ordenação
        Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderFilterDTO filter);

        // Recuperar um pedido específico com detalhes
        Task<OrderDetailDTO> GetOrderByIdAsync(int orderId);

        // Recuperar pedidos de um coprodutor específico
        Task<IEnumerable<OrderDTO>> GetOrdersByCoproducerAsync(int coproducerId);

        // Recuperar pedidos que contêm produtos de um produtor específico
        Task<IEnumerable<OrderDTO>> GetOrdersByProducerAsync(int producerId);

        // Criar um novo pedido
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO);

        // Atualizar um pedido existente
        Task<OrderDTO> UpdateOrderAsync(int orderId, UpdateOrderDTO updateOrderDTO);

        // Atualizar um item de pedido específico
        Task<OrderItemDTO> UpdateOrderItemAsync(int orderItemId, UpdateOrderItemDTO updateOrderItemDTO);

        // Adicionar um novo item a um pedido existente
        Task<OrderItemDTO> AddOrderItemAsync(int orderId, CreateOrderItemDTO createOrderItemDTO);

        // Remover um item de um pedido
        Task<bool> RemoveOrderItemAsync(int orderItemId);

        // Verificar se um coprodutor possui permissão para modificar um pedido
        Task<bool> CanCoproducerModifyOrderAsync(int coproducerId, int orderId);

        // Verificar se um produtor possui permissão para atualizar itens de um pedido
        Task<bool> CanProducerModifyOrderItemAsync(int producerId, int orderItemId);
    }
}
