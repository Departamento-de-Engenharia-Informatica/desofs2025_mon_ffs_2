using AMAPP.API.Data;
using AMAPP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AMAPP.API.Repository.CoproducerInfoRepository
{
    public class CoproducerInfoRepository : RepositoryBase<CoproducerInfo>, ICoproducerInfoRepository
    {
        public CoproducerInfoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CoproducerInfo?> GetCopoproducerInfoByUserIdAsync(string id)
        {
            return await _context.CoproducersInfo
                                 .FirstOrDefaultAsync(p => p.UserId == id);
        }

        public new async Task<IEnumerable<CoproducerInfo>> GetAllAsync()
        {
            return await _context.CoproducersInfo
                .Include(p => p.User) // Include the User property
                .ToListAsync();
        }
        
        public new IEnumerable<CoproducerInfo> GetAll()
        {
            return _context.CoproducersInfo
                .Include(p => p.User) // Include the User property
                .ToList();
        }
    }
}
