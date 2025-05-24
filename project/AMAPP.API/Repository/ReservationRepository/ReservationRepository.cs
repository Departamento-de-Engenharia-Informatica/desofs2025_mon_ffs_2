using AMAPP.API.Data;
using AMAPP.API.Models;

namespace AMAPP.API.Repository.ReservationRepository;

public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
{ 
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }
}