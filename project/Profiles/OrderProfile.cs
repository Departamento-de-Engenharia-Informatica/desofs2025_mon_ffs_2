using AutoMapper;
using AMAPP.API.DTOs.Order;
using AMAPP.API.Models;

namespace AMAPP.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Order -> OrderDTO  
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CoproducerName, opt => opt.MapFrom(src => src.CoproducerInfo.User.FirstName))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity * oi.Price)))
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.OrderItems.Count))
                .ForMember(dest => dest.ProducerNames, opt => opt.MapFrom(src => src.OrderItems
                    .Select(oi => oi.Product.ProducerInfo)
                    .Distinct()
                    .ToList()));

            // Order -> OrderDetailDTO  
            CreateMap<Order, OrderDetailDTO>()
                .ForMember(dest => dest.CoproducerName, opt => opt.MapFrom(src => src.CoproducerInfo.User.FirstName))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity * oi.Price)))
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.OrderItems.Count))
                .ForMember(dest => dest.ProducerNames, opt => opt.MapFrom(src => src.OrderItems
                    .Select(oi => oi.Product.ProducerInfo)
                    .Distinct()
                    .ToList()));

            // OrderCreateDTO -> Order  
            CreateMap<OrderCreateDTO, Order>();

            // OrderUpdateDTO -> Order  
            CreateMap<OrderUpdateDTO, Order>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // OrderItem mappings  
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProducerId, opt => opt.MapFrom(src => src.Product.ProducerInfo.UserId))
                .ForMember(dest => dest.ProducerName, opt => opt.MapFrom(src => src.Product.ProducerInfo.User.FirstName))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Product.DeliveryUnit))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Price));

            CreateMap<OrderItemCreateDTO, OrderItem>();
            CreateMap<OrderItemUpdateDTO, OrderItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Reservation mappings  
            CreateMap<Reservation, ReservationDTO>();
            CreateMap<ReservationCreateDTO, Reservation>();
            CreateMap<ReservationUpdateDTO, Reservation>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
