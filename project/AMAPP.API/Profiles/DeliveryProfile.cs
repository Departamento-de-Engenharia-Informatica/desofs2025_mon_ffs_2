using AMAPP.API.DTOs.Delivery;
using AMAPP.API.Models;
using AutoMapper;

namespace AMAPP.API.Profiles
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            // Map Delivery to DeliveryDto
            CreateMap<Delivery, DeliveryDto>();

            // Map CreateDeliveryDto to Delivery
            CreateMap<CreateDeliveryDto, Delivery>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore()); // Order navigation is handled separately

            // Map UpdateDeliveryDto to Delivery
            CreateMap<UpdateDeliveryDto, Delivery>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore()); // Order navigation is handled separately
        }
    }
}
