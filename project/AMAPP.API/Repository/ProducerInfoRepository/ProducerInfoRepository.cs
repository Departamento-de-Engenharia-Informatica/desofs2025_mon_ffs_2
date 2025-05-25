using AMAPP.API.Data;
using AMAPP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMAPP.API.Repository.ProducerInfoRepository
{
    public class ProducerInfoRepository : RepositoryBase<ProducerInfo>, IProducerInfoRepository
    {
        public ProducerInfoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ProducerInfo?> GetProducerInfoByUserIdAsync(string id)
        {
            return await _context.ProducersInfo
                                 .FirstOrDefaultAsync(p => p.UserId == id);
        }

        public new async Task<IEnumerable<ProducerInfo>> GetAllAsync()
        {
            return await _context.ProducersInfo
                .Include(p => p.User) // Include the User property
                .ToListAsync();
        }
        
        public new IEnumerable<ProducerInfo> GetAll()
        {
            return _context.ProducersInfo
                .Include(p => p.User) // Include the User property
                .ToList();
        }
    }
}
