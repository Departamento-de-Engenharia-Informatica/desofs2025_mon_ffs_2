using AMAPP.API.Data;
using AMAPP.API.Models;

namespace AMAPP.API.Repository.CoproducerInfoRepository
{
    public interface ICoproducerInfoRepository : IRepositoryBase<CoproducerInfo>
    {
        Task<CoproducerInfo?> GetCopoproducerInfoByUserIdAsync(string id);
    }
}
