using AMAPP.API.DTOs.Delivery;
using AMAPP.API.DTOs.Reservation;
using AMAPP.API.Repository.ReservationRepository;
using AMAPP.API.Services.Interfaces;

namespace AMAPP.API.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;
    
    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ReservationDto>> GetAllAsync()
    {
        var deliveries = await _repository.GetAllAsync();
        return deliveries.Select(d => new ReservationDto
        {
            Id = d.Id,
            OrderId = d.OrderId,
            Method = d.Method,
            ReservationDate = d.ReservationDate,
            Location = d.Location,
            Notes = d.Notes
        }).ToList();
    }
}