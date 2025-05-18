using AMAPP.API.Data;
using AMAPP.API.Models;

namespace AMAPP.API.Repository.ProducerInfoRepository
{
    public interface IProducerInfoRepository : IRepositoryBase<ProducerInfo>
    {
        Task<ProducerInfo?> GetProducerInfoByUserIdAsync(string id);
    }
}
