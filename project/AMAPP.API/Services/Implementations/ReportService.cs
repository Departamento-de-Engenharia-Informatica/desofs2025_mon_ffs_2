using AMAPP.API.DTOs;
using AMAPP.API.DTOs.Delivery;
using AMAPP.API.DTOs.Reservation;
using AMAPP.API.Models;
using AMAPP.API.Repository.CoproducerInfoRepository;
using AMAPP.API.Repository.DeliveryRepository;
using AMAPP.API.Repository.OrderRepository;
using AMAPP.API.Repository.ReservationRepository;
using AMAPP.API.Services.Interfaces;
using AMAPP.API.Utils;
using AutoMapper;
using QuestPDF.Fluent;

namespace AMAPP.API.Services.Implementations
{
public class ReportService : IReportService
{
    private readonly IReservationRepository _reservations;
    private readonly IDeliveryRepository    _deliveries;
    private readonly ILogger<ReportService> _logger;
    private readonly ICoproducerInfoRepository _coproducerInfo;
    private readonly IOrderRepository _orders;
    private readonly IMapper _mapper;
    private readonly ILogger<ReportDocument> _docLogger;
    

    public ReportService(
        IReservationRepository reservations,
        IDeliveryRepository    deliveries,
        ILogger<ReportService> logger,
        ICoproducerInfoRepository coproducerInfo,
        IOrderRepository orders,
        IMapper mapper,
        ILogger<ReportDocument> docLogger)
    {
        _reservations = reservations;
        _deliveries   = deliveries;
        _logger       = logger;
        _coproducerInfo = coproducerInfo;
        _orders      = orders;
        _mapper       = mapper;
        _docLogger    = docLogger;
    }
    

    public byte[] GenerateReportByUserId(string userId)
    {
        try
        {
            _logger.LogInformation("Fetching CoProducer data");
            var copInfo = _coproducerInfo.Get(ci => ci.UserId == userId)
                          ?? throw new KeyNotFoundException("CoProducerInfo not found.");

            _logger.LogInformation("Fetching orders for CoProducer");
            var orderIds = _orders
                .GetList(o => o.CoproducerInfoId == copInfo.Id)
                .Select(o => o.Id).ToList();

            if (!orderIds.Any())
                throw new InvalidOperationException("No orders found for this CoProducer.");

            _logger.LogInformation("Fetching reservations & deliveries for CoProducer");
            var userReservations = _reservations
                .GetList(r => orderIds.Contains(r.OrderId)).ToList();
            var userDeliveries = _deliveries
                .GetList(d => orderIds.Contains(d.OrderId)).ToList();

            
            var reservationDtos = _mapper.Map<List<ReservationDto>>(userReservations);
            var deliveryDtos    = _mapper.Map<List<DeliveryDto>>(userDeliveries);

            _logger.LogInformation("Building report parameters");
            var parameters = new ReportParametersDto
            {
                Title        = "Reservations & Deliveries Report",
                Date         = DateTime.Now,
                Software     = "AMAPP API",
                Username     = copInfo.User.UserName,
                
                // Assign empty lists if mapping yielded null
                Reservations = reservationDtos ?? new List<ReservationDto>(),
                Deliveries   = deliveryDtos    ?? new List<DeliveryDto>()
            };

            _logger.LogInformation("Generating PDF report");
            using var ms = new MemoryStream();
            var document = new ReportDocument(parameters, _docLogger);
            document.GeneratePdf(ms);
            return ms.ToArray();
        }
        catch (AutoMapperMappingException mapEx)
        {
            string message = "Mapping failed when generating report for CoProducer";
            _logger.LogError(mapEx, message);
            throw new ReportGenerationException(message, mapEx);
        }
        catch (Exception ex)
        {   
            string message = "Unexpected error generating report for CoProducer";   
            _logger.LogError(ex, message);
            throw new ReportGenerationException(message, ex);
        }
    }

    }

}
