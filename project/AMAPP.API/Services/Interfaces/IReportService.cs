using AMAPP.API.Models;

public interface IReportService
{
    //(List<Reservation> Reservations, List<Delivery> Deliveries) GetUserData(string username);
    
    byte[] GenerateReportByUserId(string userId);
}
