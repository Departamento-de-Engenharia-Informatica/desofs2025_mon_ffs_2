using AMAPP.API.Data;
using AMAPP.API.Models;

namespace AMAPP.API.Repository.ProdutoRepository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
