using AMAPP.API.DTOs.Reservation;

namespace AMAPP.API.Services.Interfaces;

public interface IReservationService
{
      Task<List<ReservationDto>> GetAllAsync();
}