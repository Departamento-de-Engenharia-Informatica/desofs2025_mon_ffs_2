using AutoMapper;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Models;

namespace AMAPP.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Order mappings
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CoproducerName, opt => opt.MapFrom(src => src.CoproducerInfo.User.FirstName + " " + src.CoproducerInfo.User.LastName));

            CreateMap<Order, OrderDetailDTO>();

            // OrderItem mappings  
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProducerName, opt => opt.MapFrom(src => src.Product.ProducerInfo.User.FirstName + " " + src.Product.ProducerInfo.User.LastName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            // CoproducerInfo mappings  
            CreateMap<CoproducerInfo, CoproducerInfoCreateDTO>();

            // Reservation mappings
            CreateMap<Reservation, ReservationDTO>();
        }
    }
}
