using AMAPP.API.DTOs.Delivery;

public interface IDeliveryService
{
    Task<List<DeliveryDto>> GetAllAsync();
    Task<DeliveryDto?> GetByIdAsync(int id);
    Task<DeliveryDto> CreateAsync(CreateDeliveryDto dto);
    Task<DeliveryDto?> UpdateAsync(DeliveryDto dto);
}
