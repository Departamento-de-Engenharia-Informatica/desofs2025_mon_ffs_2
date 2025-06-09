using AMAPP.API.DTOs.Order;
using AMAPP.API.DTOs.Reservation;
using AMAPP.API.Models;
using AutoMapper;

namespace AMAPP.API.Profiles;

public class ReservationProfile: Profile
{
    public ReservationProfile() 
    {

        CreateMap<Reservation, ReservationDTO>();


    }
}

