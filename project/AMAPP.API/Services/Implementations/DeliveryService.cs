using AMAPP.API.DTOs.Delivery;
using AMAPP.API.Models;
using AMAPP.API.Repository.DeliveryRepository;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _repository;

    public DeliveryService(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DeliveryDto>> GetAllAsync()
    {
        var deliveries = await _repository.GetAllAsync();
        return deliveries.Select(d => new DeliveryDto
        {
            Id = d.Id,
            OrderId = d.OrderId,
            DeliveryDate = d.DeliveryDate,
            DeliveryLocation = d.DeliveryLocation,
            Status = d.Status
        }).ToList();
    }

    public async Task<DeliveryDto?> GetByIdAsync(int id)
    {
        var d = await _repository.GetByIdAsync(id);
        if (d == null) return null;

        return new DeliveryDto
        {
            Id = d.Id,
            OrderId = d.OrderId,
            DeliveryDate = d.DeliveryDate,
            DeliveryLocation = d.DeliveryLocation,
            Status = d.Status
        };
    }

    public async Task<DeliveryDto> CreateAsync(CreateDeliveryDto dto)
    {
        var delivery = new Delivery
        {
            OrderId = dto.OrderId,
            DeliveryDate = dto.DeliveryDate,
            DeliveryLocation = dto.DeliveryLocation,
            Status = dto.Status
        };

        await _repository.AddAsync(delivery);

        return new DeliveryDto
        {
            Id = delivery.Id,
            OrderId = delivery.OrderId,
            DeliveryDate = delivery.DeliveryDate,
            DeliveryLocation = delivery.DeliveryLocation,
            Status = delivery.Status
        };
    }


    public async Task<DeliveryDto?> UpdateAsync(DeliveryDto dto)
    {
        var delivery = new Delivery
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            DeliveryDate = dto.DeliveryDate,
            DeliveryLocation = dto.DeliveryLocation,
            Status = dto.Status
        };

        await _repository.UpdateAsync(delivery);

        var updated = await _repository.GetByIdAsync(dto.Id);
        if (updated == null) return null;

        return new DeliveryDto
        {
            Id = updated.Id,
            OrderId = updated.OrderId,
            DeliveryDate = updated.DeliveryDate,
            DeliveryLocation = updated.DeliveryLocation,
            Status = updated.Status
        };
    }

}
