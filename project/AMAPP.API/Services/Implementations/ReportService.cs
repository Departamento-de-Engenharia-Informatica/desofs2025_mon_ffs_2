using AMAPP.API.DTOs;
using AMAPP.API.Repository.DeliveryRepository;
using AMAPP.API.Repository.ReservationRepository;
using AMAPP.API.Services.Interfaces;
using AMAPP.API.Utils;
using QuestPDF.Fluent;

namespace AMAPP.API.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReservationRepository _reservations;
        private readonly IDeliveryRepository _deliveries;

        public ReportService(
            IReservationRepository reservations,
            IDeliveryRepository deliveries)
        {
            _reservations = reservations;
            _deliveries = deliveries;
        }

        public byte[] GenerateReport()
        {
            // Fetch data (not used in placeholder)
            var allReservations = _reservations.GetAll();
            var allDeliveries = _deliveries.GetAll();

            // Create parameter DTO for the document
            var parameters = new ReportParametersDto
            {
                Title = "Reservations & Deliveries Report",
                Date = DateTime.Now,
                Software = "AMAPP API"
            };

            // Generate PDF
            var doc = new ReportDocument(parameters);
            using var ms = new MemoryStream();
            doc.GeneratePdf(ms);
            return ms.ToArray();
        }
    }
}
