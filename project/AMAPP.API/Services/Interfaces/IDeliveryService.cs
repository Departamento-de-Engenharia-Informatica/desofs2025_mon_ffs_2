using AMAPP.API.DTOs.Delivery;

namespace AMAPP.API.Services.Interfaces
{
    public interface IDeliveryService
    {
        // Métodos existentes
        Task<IEnumerable<DeliveryDto>> GetAllAsync();
        Task<DeliveryDto?> GetByIdAsync(int id, string? userId = null);
        Task<DeliveryDto> CreateAsync(CreateDeliveryDto dto);
        Task<DeliveryDto?> UpdateAsync(int id, UpdateDeliveryDto dto);
        Task<bool> DeleteAsync(int id);

        // Novos métodos seguindo o padrão do OrderService
        Task<IEnumerable<DeliveryDto>> GetDeliveriesByProducerAsync(int producerId);
        Task<IEnumerable<DeliveryDto>> GetDeliveriesByCoProducerAsync(int coproducerId);
        Task<IEnumerable<DeliveryDto>> GetDeliveriesByUserIdAsync(string userId);

        // Métodos de autorização
        Task<bool> IsUserAuthorizedForCoproducerAsync(string userId, int coproducerId);
        Task<bool> IsUserAuthorizedForProducerAsync(string userId, int producerId);
        Task<bool> CanUserAccessDeliveryAsync(int deliveryId, string userId);
    }
}