using AMAPP.API.Data;
using AMAPP.API.Models;

namespace AMAPP.API.Repository.DeliveryRepository
{
    public class DeliveryRepository : RepositoryBase<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
