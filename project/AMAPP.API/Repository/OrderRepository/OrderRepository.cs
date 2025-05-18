using AMAPP.API.Data;
using AMAPP.API.Models;


namespace AMAPP.API.Repository.OrderRepository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
