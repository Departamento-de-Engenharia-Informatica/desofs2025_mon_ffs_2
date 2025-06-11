using AMAPP.API.DTOs.Delivery;
using AMAPP.API.DTOs.Reservation;

namespace AMAPP.API.DTOs;
public class ReportParametersDto
{
    public string               Title        { get; set; } = string.Empty;
    public DateTime             Date         { get; set; }
    public string               Software     { get; set; } = string.Empty;
    

    public List<ReservationDto>? Reservations { get; set; } = new();
    public List<DeliveryDto>?    Deliveries   { get; set; } = new();
    
    public string?               Username     { get; set; } = string.Empty;
}
