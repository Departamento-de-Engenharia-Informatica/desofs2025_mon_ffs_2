using AMAPP.API.DTOs.SubscriptionPayment;
using AMAPP.API.Models;
using AutoMapper;

namespace AMAPP.API.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<CreateSubscriptionPaymentDto, Payment>();
            CreateMap<SubscriptionPaymentDto, Payment>();
            CreateMap<Payment, ResponseSubscriptionPaymentDto>();
            CreateMap<UpdateSubscriptionPaymentDto, ResponseSubscriptionPaymentDto>();
        }
    }
}